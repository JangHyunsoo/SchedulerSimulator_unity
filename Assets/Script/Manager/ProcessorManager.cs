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
        p_core_count_ = SceneDataManager.instance.p_core_count;
        e_core_count_ = SceneDataManager.instance.e_core_count;

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
        UIManager.instance.power_text.text = getPower().ToString();
    }

    public CoreCount countEachTypeAvailable()
    {
        CoreCount core_count = new CoreCount { e_count = 0, p_count = 0 };
        foreach (var processor in processor_arr_)
        {
            if (!processor.isRun())
            {
                if (processor.type == ProcessorType.EFFIC) core_count.e_count++;
                else core_count.p_count++;
            }
        }
        return core_count;
    }

    public int countAvailable()
    {
        int count = 0;

        foreach (Processor processor in processor_arr_)
        {
            if (!processor.isRun())
                count++;
        }
        return count;
    }

    public Processor getAvailableProcessor()
    {
        foreach (var processor in processor_arr_)
        {
            if (processor.cur_process == null)
            {
                return processor;
            }
        }

        foreach (var processor in processor_arr_)
        {
            if (!processor.isRun())
            {
                return processor;
            }
        }
        return null;
    }

    public Processor getAvailableProcessor(ProcessorType _type)
    {
        foreach (var processor in processor_arr_)
        {
            if (!processor.isRun() && processor.type == _type)
            {
                return processor;
            }
        }
        return null;
    }

    public Processor getMaxRemainingTimeProcessor()
    {
        Processor max_processor = processor_arr_[0];

        for (int i = 1; i < processor_arr_.Length; i++)
        {
            int bt_max_processor, bt_cur_processor;
            if (max_processor.cur_process == null) bt_max_processor = 987654321;
            else bt_max_processor = max_processor.cur_process.remaining_time;
            if (processor_arr_[i].cur_process == null) bt_cur_processor = 987654321;
            else bt_cur_processor = processor_arr_[i].cur_process.remaining_time;

            if (bt_max_processor < bt_cur_processor)
            {
                max_processor = processor_arr_[i];
            }
        }

        return max_processor;
    }

    public void appendGanttChart(int _cur_tick)
    {
        for (int i = 0; i < processor_count; i++)
        {
            int history = processor_arr_[i].getHistory(_cur_tick);
            UIManager.instance.processor_chart_ui.addChartUnit(history);
        }
        UIManager.instance.processor_chart_ui.addTick();
        UIManager.instance.processor_chart_ui.autoWidthSize();
    }

    public float getPower()
    {
        float ret = 0f;
        foreach (var psr in processor_arr_)
        {
            ret += psr.power_consumption;
        }

        ret = Mathf.Floor(ret * 100f) / 100f;

        return ret;
    }
}