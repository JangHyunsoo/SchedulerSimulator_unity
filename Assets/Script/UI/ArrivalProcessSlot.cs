using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrivalProcessSlot : MonoBehaviour
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
    private Text remain_time_text_;

    public void setProcess(Process process)
    {
        process_no_text_.text = process.no.ToString();
        arrival_time_text_.text = process.arrival_time.ToString();
        brust_time_text_.text = process.burst_time.ToString();
        remain_time_text_.text = process.remaining_time.ToString();

        process_panel_image_.color = JobSimulator.instance.getProcessColor(process.no);
    }
}
