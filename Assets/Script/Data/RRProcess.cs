using System;
using System.Collections.Generic;

class RRProcess : Process
{
    private int remain_quantum_time_;

    public RRProcess() : base() {
        remain_quantum_time_ = 0;
    }
    public RRProcess(Job job_) : base(job_) {
        remain_quantum_time_ = 0;
    }

    public override void init(Job _job)
    {
        base.init(_job);
        remain_quantum_time_ = 0;
    }

    public override bool isRun()
    {
        return remain_quantum_time_ > 0;
    }

    public override void tick(int _total_tick, int _work)
    {
        base.tick(_total_tick, _work);
        remain_quantum_time_ -= 1;
        if (remain_quantum_time_ < 0)
        {
            remain_quantum_time_ = 0;
        }
    }

    public void setQuantumTime(int t)
    {
        remain_quantum_time_ = t;
    }
}
