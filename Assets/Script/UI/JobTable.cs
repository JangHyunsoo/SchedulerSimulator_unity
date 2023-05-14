using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobTable : MonoBehaviour
{
    [SerializeField]
    private Transform job_slot_parent_;
    [SerializeField]
    private GameObject job_slot_prefab_;
    [SerializeField]
    private RectTransform job_table_rect_;

    private List<JobSlot> job_slot_list_ = new List<JobSlot>();
    private Dictionary<int, JobSlot> job_slot_dic_ = new Dictionary<int, JobSlot>();

    public void init()
    {
        List<Transform> del_list = new List<Transform>();
        job_slot_dic_.Clear();

        // remove job slot child
        for (int i = 0; i < job_slot_parent_.childCount; i++)
        {
            del_list.Add(job_slot_parent_.GetChild(i));
        }

        foreach (var del_go in del_list)
        {
            GameObject.Destroy(del_go);
        }

        del_list.Clear();
        job_slot_list_.Clear();

        var job_list = JobSimulator.instance.job_arr;
        foreach (var job in job_list)
        {
            addJob(job);
        }
    }
    
    public void addJob(Job _job)
    {
        GameObject go = GameObject.Instantiate(job_slot_prefab_);
        go.transform.SetParent(job_slot_parent_);
        JobSlot js = go.GetComponent<JobSlot>();
        js.setJob(_job);
        job_slot_list_.Add(js);
        job_slot_dic_[_job.job_no] = js;
    }

    public void updateRemainingTime(int _no, int _remaining_time)
    {
        job_slot_dic_[_no].setRemainingTime(_remaining_time);
    }
}
