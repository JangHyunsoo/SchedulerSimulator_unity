using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor
{
    private int processor_no_;
    private ProcessorType processor_type_;
    private Process cur_process_;
    private bool is_run_;
    private bool prev_run_;
    private float power_consumption_;
    private List<int> history_list_ = new List<int>();

    public int no { get => processor_no_; }
    public ProcessorType type { get => processor_type_; }
    public Process cur_process { get => cur_process_; }
    public float power_consumption { get => power_consumption_; }

    public Processor(int _core_no, ProcessorType _core_type = ProcessorType.EFFIC)
    {
        processor_no_ = _core_no;
        processor_type_ = _core_type;
        cur_process_ = null;
        is_run_ = false;
        prev_run_ = false;
        power_consumption_ = 0f;
        history_list_.Clear();
    }

    public void addProcess(Process _process)
    {
        cur_process_ = _process;
        is_run_ = true;
    }

    public Process swapProcess(Process _next)
    {
        Process prev = cur_process_;
        addProcess(_next);
        return prev;
    }

    public bool isRun()
    {
        if (cur_process_ == null) return false;
        else return is_run_ && cur_process_.isRun();
    }

    public void tick(int _total_tick)
    {
        if (is_run_)
        {
            if (!prev_run_) power_consumption_ += processor_type_ == ProcessorType.EFFIC ? 0.1f : 0.5f;
            power_consumption_ += processor_type_ == ProcessorType.EFFIC ? 1f : 3f;
            cur_process_.tick(_total_tick, processor_type_ == ProcessorType.EFFIC ? 1 : 2);
            history_list_.Add(cur_process_.no);
            check(_total_tick);
            prev_run_ = true;
        }
        else
        {
            prev_run_ = false;
            history_list_.Add(-1);
        }
    }

    public void check(int _total_tick)
    {
        if (cur_process_.is_dead)
        {
            cur_process_.setEndTime(_total_tick + 1);
            cur_process_.finishProcess();
            cur_process_ = null;
            is_run_ = false;
        }
    }

    public int getHistory(int _step)
    {
        return history_list_[_step];
    }

}
