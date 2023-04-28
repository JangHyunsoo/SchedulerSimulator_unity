using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUInfoUI : MonoBehaviour
{
    private Processor processor_;

    [SerializeField]
    private Text processor_type_text;
    [SerializeField]
    private Text process_no_text_;
    [SerializeField]
    private Text process_arrival_time_text_;
    [SerializeField]
    private Text process_burst_time_text_;
    [SerializeField]
    private Text process_remain_time_text_;

    public void setProcessor(Processor _processor)
    {
        processor_ = _processor;
        updateUI();
    }

    private void updateUI()
    {
        Process process = processor_.cur_process;
        processor_type_text.text = processor_.type == ProcessorType.EFFIC ? "E" : "F";
        if(process != null)
        {
            process_no_text_.text = "Process No : " + process.no.ToString();
            process_arrival_time_text_.text = "Arrival Time : " + process.arrival_time;
            process_burst_time_text_.text = "Burst Time : " + process.burst_time;
            process_remain_time_text_.text = "Remain Time : " + process.cur_burst_time;
        }
        else
        {
            process_no_text_.text = "Process No : None";
            process_arrival_time_text_.text = "Arrival Time : None";
            process_burst_time_text_.text = "Burst Time : None";
            process_remain_time_text_.text = "Remain Time : None";
        }
    }
}
