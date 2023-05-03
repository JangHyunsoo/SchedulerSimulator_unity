using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalProcessTable : MonoBehaviour
{
    [SerializeField]
    private Transform process_slot_parent_;
    [SerializeField]
    private GameObject process_slot_prefab_;
    [SerializeField]
    private RectTransform process_table_rect_;

    private List<ArrivalProcessSlot> process_slot_list_ = new List<ArrivalProcessSlot>();

    public void init()
    {
        List<Transform> del_list = new List<Transform>();

        // remove job slot child
        for (int i = 0; i < process_slot_parent_.childCount; i++)
        {
            del_list.Add(process_slot_parent_.GetChild(i));
        }

        foreach (var del_go in del_list)
        {
            GameObject.Destroy(del_go);
        }

        del_list.Clear();
        process_slot_list_.Clear();
    }

    public void updateProcesses(List<Process> processes)
    {
        foreach (var slot in process_slot_list_)
        {
            GameObject.Destroy(slot.gameObject);
        }

        process_slot_list_.Clear();

        foreach (var process in processes)
        {
            updateProcess(process);
        }
    }

    public void updateProcess(Process process)
    {
        GameObject go = GameObject.Instantiate(process_slot_prefab_);
        go.transform.SetParent(process_slot_parent_);
        ArrivalProcessSlot ps = go.GetComponent<ArrivalProcessSlot>();
        process_slot_list_.Add(ps);
        ps.setProcess(process);
        autoSize();
    }

    public void autoSize()
    {
        float rect_height = process_slot_list_.Count * 50;
        var rect = process_table_rect_.rect;

        process_table_rect_.sizeDelta = new Vector2(process_table_rect_.sizeDelta.x, rect_height);
    }
}
