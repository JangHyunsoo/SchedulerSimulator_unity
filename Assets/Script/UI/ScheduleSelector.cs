using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleSelector : MonoBehaviour
{
    private ScheduleWay[] schedule_way_arr_ = new ScheduleWay[]
    {
        ScheduleWay.FCFS,
        ScheduleWay.RR,
        ScheduleWay.SPN,
        ScheduleWay.SRTN,
        ScheduleWay.HRRN,
        ScheduleWay.OUR
    };
    private Dictionary<ScheduleWay, int> schedule_dic_ = new Dictionary<ScheduleWay, int>()
    {
        { ScheduleWay.FCFS, 0 },
        { ScheduleWay.RR, 1 },
        { ScheduleWay.SPN, 2 },
        { ScheduleWay.SRTN, 3 },
        { ScheduleWay.HRRN, 4 },
        { ScheduleWay.OUR, 5 },

    };
    private int cur_schedule_way_idx_ = 0;

    [SerializeField]
    private Image[] schedule_button_arr_;
    [SerializeField]
    private Color selected_color_;

    public void init(ScheduleWay _way)
    {
        cur_schedule_way_idx_ = schedule_dic_[_way];
        clear();
        checkSchedule(cur_schedule_way_idx_);
    }

    private void clear()
    {
        foreach (var image in schedule_button_arr_)
        {
            image.color = Color.white;
        }
    }

    public void checkSchedule(int _idx)
    {
        clear();
        schedule_button_arr_[_idx].color = selected_color_;
        cur_schedule_way_idx_ = _idx;
        SetUpManager.instance.setSchedule(schedule_way_arr_[_idx]);

    }
}
