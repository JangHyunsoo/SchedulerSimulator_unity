using System;
using System.Collections.Generic;
using UnityEngine;

public class HRRNScheduler : Scheduler
{
    public class HRRNCompare : IComparer<Process>
    {
        public int Compare(Process x, Process y)
        {
            if (x.response_ratio == y.response_ratio)
            {
                if (y.burst_time == x.burst_time) return 0;
                else return y.burst_time > x.burst_time ? 1 : -1;
            }
            else return (y.response_ratio < x.response_ratio) ? 1 : -1;
        }
    }

    private PriorityQueue<Process> process_queue_ = new PriorityQueue<Process>(new HRRNCompare());

    public override void init()
    {
        process_queue_.clear();
    }

    public override void queuing(int _total_tick)
    {
        Queue<Process> arrival_process_queue = getArrivalProcess(_total_tick);
        PriorityQueue<Process> temp_queue_ = new PriorityQueue<Process>(new HRRNCompare());

        while (!process_queue_.empty)
        {
            var process = process_queue_.pop();
            process.setResponseRatio(_total_tick);
            temp_queue_.push(process);
        }

        while (arrival_process_queue.Count != 0)
        {
            var process = arrival_process_queue.Dequeue();
            process.setResponseRatio(_total_tick);
            temp_queue_.push(process);
        }

        process_queue_ = temp_queue_;

        UIManager.instance.chart_process_queue_ui.updateUI(process_queue_.ToArray());
    }

    public override void logic(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;

        while (psr_mgr.canUse() && process_queue_.count != 0)
        {
            psr_mgr.addProcess(process_queue_.pop());
        }

        psr_mgr.tick(_total_tick);

        queuing(_total_tick);
    }
}
