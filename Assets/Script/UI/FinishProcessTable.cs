using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishProcessTable : MonoBehaviour
{
    [SerializeField]
    private Transform process_slot_parent_;
    [SerializeField]
    private GameObject process_slot_prefab_;
    [SerializeField]
    private RectTransform process_table_rect_;

    private List<FinishProcessSlot> process_slot_list_ = new List<FinishProcessSlot>();

    public void init()
    {
        List<Transform> del_list = new List<Transform>();

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
        FinishProcessSlot ps = go.GetComponent<FinishProcessSlot>();
        process_slot_list_.Add(ps);
        ps.setProcess(process);
    }
}
