using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleInfoDisplay : MonoBehaviour
{
    Dictionary<ScheduleWay, string> scheduleWays = new Dictionary<ScheduleWay, string>()
    {
        { ScheduleWay.FCFS, "FCFS" },
        { ScheduleWay.RR, "RR" },
        { ScheduleWay.SPN, "SPN" },
        { ScheduleWay.SRTN, "SRTN" },
        { ScheduleWay.HRRN, "HRRN" },
        { ScheduleWay.DPS, "DPS" }
    };


    [SerializeField]
    private Text scheduler_name_text_;
    [SerializeField]
    private Text quantum_time_text_;
    [SerializeField]
    private Text p_core_count_text_;
    [SerializeField]
    private Text e_core_count_text_;
    [SerializeField]
    private Text p_core_power_text_;
    [SerializeField]
    private Text e_core_power_text_;


    public void init()
    {
        updateUI();
    }
    public void updateUI()
    {
        scheduler_name_text_.text = scheduleWays[SceneDataManager.instance.schedule_way];
        quantum_time_text_.text = SceneDataManager.instance.time_quantum.ToString();
        p_core_count_text_.text = SceneDataManager.instance.p_core_count.ToString();
        e_core_count_text_.text = SceneDataManager.instance.e_core_count.ToString();
        p_core_power_text_.text = ProcessorManager.instance.getPower(ProcessorType.PERFOR).ToString();
        e_core_power_text_.text = ProcessorManager.instance.getPower(ProcessorType.EFFIC).ToString();
    }
}
