using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSlot : MonoBehaviour
{
    [SerializeField]
    private Image job_panel_image_;
    [SerializeField]
    private Text job_no_text_;
    [SerializeField]
    private Text arrival_time_text_;
    [SerializeField]
    private Text brust_time_text_;


    public void setJob(Job job)
    {
        job_no_text_.text = job.job_no.ToString();
        arrival_time_text_.text = job.arrival_time.ToString();
        brust_time_text_.text = job.brust_time.ToString();
        job_panel_image_.color = JobSimulator.instance.getProcessColor(job.job_no);
    }
}
