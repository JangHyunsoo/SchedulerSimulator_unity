using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartProcessQueueUI : MonoBehaviour
{
    [SerializeField]
    private GameObject unit_prefab_;
    [SerializeField]
    private Transform on_process_queue_parent_;
    [SerializeField]
    private Transform rest_process_parent_;

    private Dictionary<int, GameObject> ready_process_dic_ = new Dictionary<int, GameObject>();
    private Process[] cur_ready_process_ = new Process[0]; 

    public void init()
    {
        int job_size = JobSimulator.instance.job_size;

        for (int i = 0; i < job_size; i++)
        {
            GameObject go = GameObject.Instantiate(unit_prefab_);
            go.GetComponent<Image>().color = JobSimulator.instance.getProcessColor(i);
            ready_process_dic_[i] = go;
            setRest(ready_process_dic_[i]);
        }
    }

    public void updateUI(Process[] processes)
    {
        cur_ready_process_ = processes;

        clearGrid();

        foreach (var process in cur_ready_process_)
        {
            setReady(ready_process_dic_[process.no]);
        }
    }

    public void clearGrid()
    {
        int job_size = JobSimulator.instance.job_size;

        for (int i = 0; i < job_size; i++)
        {
            setRest(ready_process_dic_[i]);
        }
    }

    public void setReady(GameObject go)
    {
        go.transform.SetParent(on_process_queue_parent_);
        go.active = true;
    }

    public void setRest(GameObject go)
    {
        go.transform.SetParent(rest_process_parent_);
        go.active = false;
    }

}
