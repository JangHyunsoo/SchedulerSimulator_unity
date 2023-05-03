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


    public void init()
    {
        List<Transform> del_list = new List<Transform>();

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
    }
    
    public void addJob(Job _job)
    {
        GameObject go = GameObject.Instantiate(job_slot_prefab_);
        go.transform.SetParent(job_slot_parent_);
        JobSlot js = go.GetComponent<JobSlot>();
        js.setJob(_job);
        job_slot_list_.Add(js);
        autoSize();
    }

    public void autoSize()
    {
        float rect_height = job_slot_list_.Count * 50;
        var rect = job_table_rect_.rect;
 
        job_table_rect_.sizeDelta = new Vector2(job_table_rect_.sizeDelta.x, rect_height);
    }
}
