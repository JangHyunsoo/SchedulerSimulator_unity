using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Scenario : ScriptableObject
{
    public string scenario_name;

    public int e_core_count;
    public int p_core_count;
    public ScheduleWay schedule_way;
    public int quantum_time;

    public Job[] jobs;
    public Color[] colors;
}
