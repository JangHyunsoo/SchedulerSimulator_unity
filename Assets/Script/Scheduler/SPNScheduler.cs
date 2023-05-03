using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPNScheduler : Scheduler
{
    public class SPNCompare : IComparer<Process>
    {
        public int Compare(Process x, Process y)
        {
            if (x.burst_time == y.burst_time)
            {
                return y.no.CompareTo(x.no);
            }
            else return y.burst_time - x.burst_time;
        }
    }

    private PriorityQueue<Process> process_queue_ = new PriorityQueue<Process>(new SPNCompare());

    public override void init()
    {
        process_queue_.clear();
    }

    public override void logic(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;

        Queue<Process> arrival_process_queue = getArrivalProcess(_total_tick);

        while (arrival_process_queue.Count != 0)
        {
            process_queue_.push(arrival_process_queue.Dequeue());
        }

        while (psr_mgr.canUse() && process_queue_.count != 0)
        {
            psr_mgr.addProcess(process_queue_.pop());
        }

        psr_mgr.tick(_total_tick);
    }
}
