using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessorManager : Singleton<ProcessorManager>
{
    [SerializeField]
    private int p_core_count_ = 2;
    [SerializeField]
    private int e_core_count_ = 2;
    private Processor[] processor_arr_;

    public int processor_count { get => p_core_count_ + e_core_count_; }

    public void init()
    {
        processor_arr_ = new Processor[p_core_count_ + e_core_count_];
        for (int i = 0; i < p_core_count_; i++)
        {
            processor_arr_[i] = new Processor(i, ProcessorType.PERFOR);
        }
        for (int i = p_core_count_; i < p_core_count_ + e_core_count_; i++)
        {
            processor_arr_[i] = new Processor(i, ProcessorType.EFFIC);
        }
    }
    public void tick(int total_tick)
    {
        foreach (Processor processor in processor_arr_)
        {
            processor.tick(total_tick);
        }
    }

    public bool isDone()
    {
        foreach (Processor processor in processor_arr_)
        {
            if (processor.is_run) return false;
        }
        return true;
    }

    public int countAvailable()
    {
        int count = 0;

        foreach (Processor processor in processor_arr_)
        {
            if (!processor.is_run)
                count++;
        }
        return count;
    }

    public bool canUse()
    {
        foreach (Processor processor in processor_arr_)
        {
            if (!processor.is_run)
                return true;
        }
        return false;
    }

    public int countQuantumAvailable()
    {
        int count = 0;
        foreach (var processor in processor_arr_)
        {
            if (!processor.isRunQuantum())
            {
                count++;
            }
        }
        return count;
    }

    public Processor getAvailableProcessor()
    {
        foreach (var processor in processor_arr_)
        {
            if (!processor.is_run)
            {
                return processor;
            }
        }
        return null;
    }

    public Processor getQuantumAvailableProcessor()
    {
        foreach (var processor in processor_arr_)
        {
            if (!processor.isRunQuantum())
            {
                return processor;
            }
        }
        return null;
    }

    public KeyValuePair<int, int> maxCurBurstTime()
    {
        int max_idx = -1;
        int max_value = -1;

        for (int i = 0; i < processor_arr_.Length; i++)
        {
            int burst_time = 987654321;

            if (processor_arr_[i].is_run)
            {
                burst_time = processor_arr_[i].cur_process.cur_burst_time;
            }

            if (burst_time > max_value)
            {
                max_idx = i;
                max_value = burst_time;
            }
        }

        return new KeyValuePair<int, int>(max_idx, max_value);
    }

    public Processor getProcessor(int idx)
    {
        if (processor_arr_.Length <= idx)
        {
            return null;
        }
        return processor_arr_[idx];
    }

    public bool addProcess(Process _process)
    {
        foreach (Processor processor in processor_arr_)
        {
            if (!processor.is_run)
            {
                processor.addProcess(_process);
                return true;
            }
        }
        return false;
    }

    public void appendGanttChart(int _cur_tick)
    {
        for (int i = 0; i < processor_count; i++)
        {
            int history = processor_arr_[i].getHistory(_cur_tick);
            UIManager.instance.processor_chart_ui.addChartUnit(history);
        }
        UIManager.instance.processor_chart_ui.addTick();
        UIManager.instance.processor_chart_ui.autoSize();
    }

    public void updateGanttChart(int _total_tick)
    {
        for (int i = 0; i < _total_tick; i++)
        {
            for (int j = 0; j < processor_count; j++)
            {
                int history = processor_arr_[j].getHistory(i);
                UIManager.instance.processor_chart_ui.addChartUnit(history);
            }
        }
        UIManager.instance.processor_chart_ui.autoSize();
    }
}