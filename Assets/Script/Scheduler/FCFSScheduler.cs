using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCFSScheduler : Scheduler
{
    private Queue<Process> process_queue_ = new Queue<Process>();

    public override void init()
    {
        process_queue_.Clear();
    }

    public override void queuing(int _total_tick)
    {
        Queue<GeneralProcess> arrival_process_queue = getArrivalProcess<GeneralProcess>(_total_tick);

        while (arrival_process_queue.Count != 0)
        {
            process_queue_.Enqueue(arrival_process_queue.Dequeue());
        }

        UIManager.instance.chart_process_queue_ui.updateUI(process_queue_.ToArray());
    }

    public override void tick(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;

        while (psr_mgr.countAvailable() > 0 && process_queue_.Count != 0)
        {
            var processor = psr_mgr.getAvailableProcessor();
            processor.addProcess(process_queue_.Dequeue());
        }

        psr_mgr.tick(_total_tick);
    }
}