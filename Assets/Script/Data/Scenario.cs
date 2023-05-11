using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : ScriptableObject
{
    public int e_core_count;
    public int p_core_count;
    public ScheduleWay schedule_way;
    public int quantum_time;

    public Job[] jobs;
}
