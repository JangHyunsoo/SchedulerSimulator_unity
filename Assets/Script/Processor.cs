using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor
{
    private int processor_no_;
    private ProcessorType processor_type_;
    private Process cur_process_;
    private bool is_run_;
    private List<int> history_list_ = new List<int>();

    public int no { get => processor_no_; }
    public ProcessorType type { get => processor_type_; }
    public Process cur_process { get => cur_process_; }
    public bool is_run { get => cur_process_ != null && is_run_; }

    public Processor(int _core_no, ProcessorType _core_type = ProcessorType.EFFIC)
    {
        processor_no_ = _core_no;
        processor_type_ = _core_type;
        cur_process_ = null;
        is_run_ = false;
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
    
    public bool isRunQuantum()
    {
        if (cur_process_ == null) return false;
        else return is_run_ && !cur_process_.end_quantum;
    }

    public void tick(int _total_tick)
    {
        if (is_run)
        {
            cur_process_.tick(_total_tick, processor_type_ == ProcessorType.EFFIC ? 1 : 2);
            history_list_.Add(cur_process_.no);
            if (cur_process_.is_dead)
            {
                cur_process_.setEndTime(_total_tick + 1);
                UIManager.instance.finish_process_table_ui.updateProcess(cur_process);
                cur_process_ = null;
                is_run_ = false;
            }
        }
        else
        {
            history_list_.Add(-1);
        }
    }

    public int getHistory(int _step)
    {
        return history_list_[_step];
    }

}
