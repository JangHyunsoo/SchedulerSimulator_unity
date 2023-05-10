using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiThreadScheduler : Scheduler
{
    public struct AllocData
    {
        public int burst_time;
        public ProcessorType core_type;
    }

    private Queue<Process> process_queue_ = new Queue<Process>();
    private Queue<Process> merge_process_queue_ = new Queue<Process>();

    public override void init()
    {
        process_queue_.Clear();
        merge_process_queue_.Clear();
    }

    public override void queuing(int _total_tick)
    {
        Queue<MultiProcess> arrival_process_queue = getArrivalProcess<MultiProcess>(_total_tick);

        while (arrival_process_queue.Count != 0)
        {
            process_queue_.Enqueue(arrival_process_queue.Dequeue());
        }

        UIManager.instance.chart_process_queue_ui.updateUI(process_queue_.ToArray());
    }

    public override void logic(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;

        while (merge_process_queue_.Count != 0 && psr_mgr.canUse())
        {
            var core_count = psr_mgr.countEachTypeAvailable();
            // E-Core ¿ì¼±
            if (core_count.e_count > 0)
            {
                var processor = psr_mgr.getAvailableProcessor(ProcessorType.EFFIC);
                processor.addProcess(merge_process_queue_.Dequeue());
            }
            else
            {
                var processor = psr_mgr.getAvailableProcessor(ProcessorType.PERFOR);
                processor.addProcess(merge_process_queue_.Dequeue());
            }
        }

        while (process_queue_.Count != 0 && psr_mgr.canUse())
        {
            MultiProcess process = (MultiProcess)process_queue_.Dequeue();
            if (psr_mgr.countAvailable() >= 2)
            {
                int burst_time = process.burst_time;
                CoreCount core_count = psr_mgr.countEachTypeAvailable();
                List<AllocData> alloc_list = dividProcess(core_count, burst_time);
                if(isBenefit(alloc_list, burst_time))
                {
                    process.setResponseTime(_total_tick);
                    foreach (var alloc_data in alloc_list)
                    {
                        var processor = psr_mgr.getAvailableProcessor(alloc_data.core_type);
                        processor.addProcess(process.makeSubProcess(alloc_data.burst_time));
                    }
                }
                else
                {
                    psr_mgr.addProcess(process);
                }
            }
            else
            {
                psr_mgr.addProcess(process);
            }
        }

        psr_mgr.tick(_total_tick);
    }

    private List<AllocData> dividProcess(CoreCount _core_count, int _burst_time)
    {
        List<AllocData> ret = new List<AllocData>();
        int e_count = _core_count.e_count;
        int p_count = _core_count.p_count;
        int total_perfor = e_count + p_count * 2;

        int share_tick = _burst_time / total_perfor;
        int mod_tick = _burst_time % total_perfor;

        if (mod_tick % 2 == 1)
        {
            if (_core_count.e_count > 0)
            {
                ret.Add(new AllocData { burst_time = 1 + share_tick, core_type = ProcessorType.EFFIC });
                mod_tick -= 1;
                e_count--;
            }
        }

        while (mod_tick > 0)
        {
            if (p_count > 0)
            {
                ret.Add(new AllocData { burst_time = 2 + share_tick * 2, core_type = ProcessorType.PERFOR });
                mod_tick -= 2;
                p_count--;
            }
            else
            {
                ret.Add(new AllocData { burst_time = 1 + share_tick, core_type = ProcessorType.EFFIC });
                mod_tick -= 1;
                e_count--;
            }
        }

        
        while(p_count + e_count > 0 && share_tick > 0)
        {
            if (p_count > 0)
            {
                ret.Add(new AllocData { burst_time = share_tick * 2, core_type = ProcessorType.PERFOR });
                p_count--;
            }
            else
            {
                ret.Add(new AllocData { burst_time = share_tick, core_type = ProcessorType.EFFIC });
                e_count--;
            }
        }

        return ret;
    }

    private bool isBenefit(List<AllocData> _alloc_data_list, int _burst_time)
    {
        int multi_burst = 0;
        int core_perform = 1;
        
        foreach (var alloc_data in _alloc_data_list)
        {
            int type_perform = alloc_data.core_type == ProcessorType.EFFIC ? 1 : 2;
            int sub_brust_time = alloc_data.burst_time / type_perform + alloc_data.burst_time % type_perform;
            if (multi_burst < sub_brust_time)
            {
                multi_burst = sub_brust_time;
            }
            if(type_perform == 2)
            {
                core_perform = 2;
            }
        }

        int single_burst_time = _burst_time / core_perform + _burst_time % core_perform;

        return multi_burst + 1 < single_burst_time;
    }

    public void addMergeProcess(MultiProcess process)
    {
        merge_process_queue_.Enqueue(process);
    }
}
