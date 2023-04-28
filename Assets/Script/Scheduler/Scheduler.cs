using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler
{
    public virtual void init() { }
    public virtual void logic(int _total_tick) { }
    public void tick(int _total_tick)
    {
        logic(_total_tick);
    }

    protected Queue<Process> getArrivalProcess(int _total_tick)
    {
        Queue<Process> ret = new Queue<Process>();
        Queue<Job> arrival_job = JobSimulator.instance.getJobs(_total_tick);

        while (arrival_job.Count != 0)
        {
            ret.Enqueue(new Process(arrival_job.Dequeue()));
        }

        return ret;
    }
}
