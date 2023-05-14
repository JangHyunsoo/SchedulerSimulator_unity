using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTable : MonoBehaviour
{
    [SerializeField]
    private GameObject result_slot_prefab_;
    [SerializeField]
    private Transform result_slot_parent_;
    [SerializeField]
    private ResultSlot cur_result_slot_;
    
    public void setCurResultSlot()
    {
        var info = ResultBuilder.instance.getCurResultInfo();
        cur_result_slot_.setResultInfo(info);
        var results = SceneDataManager.instance.getResultInfo();

        foreach (var result in results)
        {
            addResult(result);
        }
    }

    private void addResult(ResultInfo result)
    {
        var go = GameObject.Instantiate(result_slot_prefab_);
        go.GetComponent<ResultSlot>().setResultInfo(result);
        go.transform.SetParent(result_slot_parent_);
    }

}
