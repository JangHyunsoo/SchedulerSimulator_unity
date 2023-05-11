using System;
using System.Collections.Generic;

public class SRTNScheduler : Scheduler
{
    public class STRNCompare : IComparer<Process>
    {
        public int Compare(Process one, Process other)
        {
            if (one.remaining_time == other.remaining_time)
            {
                return 0;
            }
            else return other.remaining_time - one.remaining_time;
        }
    }

    private PriorityQueue<Process> process_queue_ = new PriorityQueue<Process>(new STRNCompare());

    public override void init()
    {
        process_queue_.clear();
    }

    public override void queuing(int _total_tick)
    {
        Queue<GeneralProcess> arrival_process_queue = getArrivalProcess<GeneralProcess>(_total_tick);

        while (arrival_process_queue.Count != 0)
        {
            process_queue_.push(arrival_process_queue.Dequeue());
        }

        UIManager.instance.chart_process_queue_ui.updateUI(process_queue_.ToArray());
    }

    public override void tick(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;

        while (process_queue_.count != 0)
        {
            var processor = psr_mgr.getMaxRemainingTimeProcessor();
            var process = process_queue_.peek();
            int rm_processor;
            if (processor.cur_process == null) rm_processor = 987654321;
            else rm_processor = processor.cur_process.remaining_time;

            if (rm_processor <= process.remaining_time)
            {
                break;
            }
            else
            {
                Process swap_process = processor.swapProcess(process);
                process_queue_.pop();
                if(swap_process != null)
                {
                    process_queue_.push(swap_process);
                }
            }
        }

        psr_mgr.tick(_total_tick);
    }
}
