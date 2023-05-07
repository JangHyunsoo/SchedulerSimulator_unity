using System;
using System.Collections;
using System.Collections.Generic;

public class MultiThreadScheduler : Scheduler
{
    private Queue<Process> process_queue_ = new Queue<Process>();

    public override void init()
    {
        process_queue_.Clear();
    }

    public override void queuing(int _total_tick)
    {
        Queue<Process> arrival_process_queue = getArrivalProcess(_total_tick);

        while (arrival_process_queue.Count != 0)
        {
            process_queue_.Enqueue(arrival_process_queue.Dequeue());
        }

        UIManager.instance.chart_process_queue_ui.updateUI(process_queue_.ToArray());
    }

    public override void logic(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;


        while(process_queue_.Count != 0 && psr_mgr.canUse())
        {
            Process process = process_queue_.Dequeue();
            int remain_time = process.burst_time;

            while (psr_mgr.canUse() && remain_time > 0)
            {
                var processor = psr_mgr.getAvailableProcessor();
                remain_time -= processor.type == ProcessorType.EFFIC ? 1 : 2;
                processor.addProcess(process);
            }
        }
        

        psr_mgr.tick(_total_tick);
    }
}
