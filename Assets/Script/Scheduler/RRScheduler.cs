using System.Collections;
using System.Collections.Generic;

class RRScheduler : Scheduler
{
    private int time_quantum_;

    public RRScheduler(int _time_quantum)
    {
        time_quantum_ = _time_quantum;
    }
}