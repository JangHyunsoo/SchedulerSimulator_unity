using System;
using System.Collections.Generic;

class GeneralProcess : Process
{
    public GeneralProcess() : base() { }
    public GeneralProcess(Job job_) : base(job_) { }

    public override void init(Job _job)
    {
        base.init(_job);
    }
}
