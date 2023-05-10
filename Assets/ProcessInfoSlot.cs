using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessInfoSlot : MonoBehaviour
{
    [SerializeField] 
    private Image process_image_;
    [SerializeField]
    private Text no_text_;
    [SerializeField]
    private Text bt_text_;
    [SerializeField]
    private Text at_text_;

    private Job job_;

    public void setJob(Job _job)
    {
        job_ = _job;
        process_image_.color = SetUpManager.instance.getJobColor(_job.job_no);
        no_text_.text = _job.job_no.ToString();
        at_text_.text = _job.arrival_time.ToString();
        bt_text_.text = _job.brust_time.ToString();
    }

    public void updateUI()
    {
        no_text_.text = job_.job_no.ToString();
        process_image_.color = SetUpManager.instance.getJobColor(job_.job_no);
        at_text_.text = job_.arrival_time.ToString();
        bt_text_.text = job_.brust_time.ToString();
    }

    public void onClickDelete()
    {
        SetUpManager.instance.deleteJob(job_.job_no);
        Destroy(gameObject);
    }
}
