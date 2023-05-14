using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishProcessSlot : MonoBehaviour
{
    [SerializeField]
    private Image process_panel_image_;
    [SerializeField]
    private Text process_no_text_;
    [SerializeField]
    private Text arrival_time_text_;
    [SerializeField]
    private Text brust_time_text_;
    [SerializeField]
    private Text waiting_time_text_;
    [SerializeField]
    private Text total_time_text_;
    [SerializeField]
    private Text normalized_total_time_text_;

    public void setProcess(Process process)
    {
        process_no_text_.text = process.no.ToString();
        arrival_time_text_.text = process.arrival_time.ToString();
        brust_time_text_.text = process.burst_time.ToString();
        waiting_time_text_.text = process.waiting_time.ToString();
        total_time_text_.text = process.turn_around_time.ToString();
        normalized_total_time_text_.text = (Mathf.Round(process.normalized_turn_around_time * 100f) / 100f).ToString();

        process_panel_image_.color = JobSimulator.instance.getProcessColor(process.no);
    }
}
