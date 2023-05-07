using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubProcess : Process
{
    public MultiProcess parent_process_;

    public SubProcess() : base() { }
    public SubProcess(Job _job) : base(_job) { }
    public SubProcess(int _no, int _burst_time) : base(_no, _burst_time) { }

    public void setParent(MultiProcess _parent)
    {
        parent_process_ = _parent;
    }

    public override void finishProcess()
    {
        parent_process_.finshChild();
    }

    public override void tick(int _total_tick, int _work)
    {
        base.tick(_total_tick, _work);
    }
}
