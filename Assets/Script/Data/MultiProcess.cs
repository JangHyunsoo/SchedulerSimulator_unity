using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiProcess : Process
{
    public MultiProcess() : base() { }
    public MultiProcess(Job job_) : base(job_) { }

    public override void init(Job _job)
    {
        base.init(_job);
    }

    private int divided_process_ = 0;
    private int complete_process_ = 0;
}