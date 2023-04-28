using System;
using System.Collections.Generic;

public class STRNScheduler : Scheduler
{
    public class CompareProcess : IComparer<Process>
    {
        public int Compare(Process one, Process other)
        {
            if (one.arrival_time == other.arrival_time)
            {
                return one.no - other.no;
            }
            else return one.arrival_time - other.arrival_time;
        }
    }

    private PriorityQueue<Process> process_pq_ = new PriorityQueue<Process>(new CompareProcess());

    public override void init()
    {
        process_pq_.clear();
    }

    public override void logic(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;
        Queue<Process> arrival_process_queue = getArrivalProcess(_total_tick);

        while (arrival_process_queue.Count != 0) { }
        {

        }
    }
}
