using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataManager : Singleton<SceneDataManager>
{
    private Job[] job_arr_;
    private ScheduleWay schedule_way_ = ScheduleWay.FCFS;
    private int time_quantum_ = 2;
    private int e_core_count_ = 2;
    private int p_core_count_ = 2;

    public void init()
    {

    }
}
