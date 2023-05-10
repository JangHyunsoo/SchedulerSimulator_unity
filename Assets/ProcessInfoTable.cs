using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessInfoTable : MonoBehaviour
{
    [SerializeField]
    private GameObject slot_prefab_;
    [SerializeField]
    private GameObject add_slot_;
    [SerializeField]
    private Transform slot_parent_;
    [SerializeField]
    private Transform add_parent_;
    [SerializeField]
    private ScrollRect slot_scroll_rect_;

    private List<ProcessInfoSlot> job_slot_list_ = new List<ProcessInfoSlot>();

    public void init()
    {


    }

    public void addJob(Job _job)
    {
        add_slot_.transform.SetParent(add_parent_);
        var go = GameObject.Instantiate(slot_prefab_);
        go.transform.SetParent(slot_parent_);
        add_slot_.transform.SetParent(slot_parent_);
        var slot = go.GetComponent<ProcessInfoSlot>();
        slot.setJob(_job);
        job_slot_list_.Add(slot);
        autoSize();
        slot_scroll_rect_.verticalNormalizedPosition = 0f;
    }

    public void removeJob(int _no)
    {
        job_slot_list_.RemoveAt(_no);
        foreach (var job_slot in job_slot_list_)
        {
            job_slot.updateUI();
        }
        autoSize();
    }

    public void autoSize()
    {
        float rect_height = (job_slot_list_.Count + 1) * 62 + 2;
        var rect = slot_scroll_rect_.content.rect;

        slot_scroll_rect_.content.sizeDelta = new Vector2(slot_scroll_rect_.content.sizeDelta.x, rect_height);
    }
}
