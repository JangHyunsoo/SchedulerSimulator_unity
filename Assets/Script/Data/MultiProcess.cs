using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiProcess : Process
{
    private int divided_process_ = 0;
    private int complete_process_ = 0;

    public MultiProcess() : base() { }
    public MultiProcess(Job _job) : base(_job) { }

    public override void init(Job _job)
    {
        base.init(_job);
        divided_process_ = 0;
        complete_process_ = 0;
    }

    public SubProcess makeSubProcess(int _burst_time)
    {
        SubProcess sub_process = new SubProcess(no, _burst_time);
        sub_process.setParent(this);
        divided_process_++;
        return sub_process;
    }

    public void finshChild()
    {
        complete_process_++;
        if (complete_process_ == divided_process_)
        {
            MultiThreadScheduler scheduler = (MultiThreadScheduler)SchedulerManager.instance.cur_scheduler;
            setBurstTime(1);
            scheduler.addMergeProcess(this);
        }
    }
}