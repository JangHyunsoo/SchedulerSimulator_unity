using System.Collections;
using System.Collections.Generic;

public class Scheduler
{
    public virtual void init() { }
    public virtual void queuing(int _total_tick) { }
    public virtual void tick(int _total_tick) { }

    protected Queue<T> getArrivalProcess<T>(int _total_tick) where T : Process, new()
    {
        Queue<T> ret = new Queue<T>();
        Queue<Job> arrival_job = JobSimulator.instance.getJobs(_total_tick);

        while (arrival_job.Count != 0)
        {
            T process = new T();
            process.init(arrival_job.Dequeue());
            ret.Enqueue(process);
        }

        return ret;
    }
}
