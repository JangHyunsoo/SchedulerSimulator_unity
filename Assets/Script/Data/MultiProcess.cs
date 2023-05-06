using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiProcess : Process
{
    

    public MultiProcess(Job _job) : base(_job) { }

    public void completeProcess()
    {
        completed_process_count_++;
        if (completed_process_count_ == divided_process_count_) need_merge_ = true;
    }
}
