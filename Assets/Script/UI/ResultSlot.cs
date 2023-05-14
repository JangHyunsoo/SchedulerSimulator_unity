using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSlot : MonoBehaviour
{
    [SerializeField]
    private Text scheduling_way_text_;
    [SerializeField]
    private Text p_core_count_text_;
    [SerializeField]
    private Text e_core_count_text_;
    [SerializeField]
    private Text core_count__text_;
    [SerializeField]
    private Text process_count_text_;
    [SerializeField]
    private Text at_mean_text_;
    [SerializeField]
    private Text at_stdev_text_;
    [SerializeField]
    private Text bt_mean_text_;
    [SerializeField]
    private Text bt_stdev_text_;
    [SerializeField]
    private Text waiting_time_mean_text_;
    [SerializeField]
    private Text turn_around_time_mean_text_;
    [SerializeField]
    private Text normalized_turn_around_time_mean_text_;

    public void setResultInfo(ResultInfo _result)
    {
        scheduling_way_text_.text = Utility.schedule_dic_[_result.schedule_way];
        p_core_count_text_.text = _result.p_core_count.ToString();
        e_core_count_text_.text = _result.e_core_count.ToString();
        core_count__text_.text = _result.core_count.ToString();
        process_count_text_ .text = _result.process_count.ToString();
        at_mean_text_.text= (Mathf.Round(_result.at_mean * 100f) / 100f).ToString();
        at_stdev_text_.text = (Mathf.Round(_result.at_stdev * 100f) / 100f).ToString();
        bt_mean_text_.text = (Mathf.Round(_result.bt_mean * 100f) / 100f).ToString();
        bt_stdev_text_ .text = (Mathf.Round(_result.bt_stdev * 100f) / 100f).ToString();
        waiting_time_mean_text_.text = (Mathf.Round(_result.waiting_time_mean * 100f) / 100f).ToString();
        turn_around_time_mean_text_.text = (Mathf.Round(_result.turn_around_time_mean * 100f) / 100f).ToString();
        normalized_turn_around_time_mean_text_.text = (Mathf.Round(_result.normalized_turn_around_time_mean * 100f) / 100f).ToString();
    }
}
