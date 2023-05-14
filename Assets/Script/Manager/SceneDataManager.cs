using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataManager : Singleton<SceneDataManager>
{
    private Job[] job_arr_ = new Job[0];
    private Color[] job_color_arr_ = new Color[0];
    private ScheduleWay schedule_way_ = ScheduleWay.FCFS;
    public ScheduleWay schedule_way { get => schedule_way_; set => schedule_way_ = value; }

    private int time_quantum_ = 2;
    public int time_quantum { get => time_quantum_; set => time_quantum_ = value; }

    private int e_core_count_ = 2;
    public int e_core_count { get => e_core_count_; set => e_core_count_ = value; }

    private int p_core_count_ = 2;
    public int p_core_count { get => p_core_count_; set => p_core_count_ = value; }

    private List<ResultInfo> results = new List<ResultInfo>();

    public void init()
    {
    }

    public void addResult(ResultInfo result)
    {
        results.Add(result);
    }

    public List<ResultInfo> getResultInfo()
    {
        return results;
    }

    public Job[] getJobs()
    {
        return job_arr_;
    }    

    public Color[] getJobColors()
    {
        return job_color_arr_;
    }

    public void setJobArr(Job[] _job_arr)
    {
        job_arr_ = _job_arr;
    }

    public void setJobColorArr(Color[] _colors)
    {
        job_color_arr_ = _colors;
    }

    public void setPCore(int _p_count)
    {
        p_core_count_ = _p_count;
    }

    public void setECore(int _e_count)
    {
        e_core_count_ = _e_count;
    }

    public void setScheduleWay(ScheduleWay _way, int _time_quantum)
    {
        schedule_way_ = _way;
        time_quantum_ = _time_quantum;
    }

    public void setScenario(Scenario _scenario)
    {
        setJobArr(_scenario.jobs);
        setJobColorArr(_scenario.colors);
        setPCore(_scenario.p_core_count);
        setECore(_scenario.e_core_count);
        setScheduleWay(_scenario.schedule_way, _scenario.quantum_time);
    }
}
