using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenarioTable : MonoBehaviour
{
    [SerializeField]
    private GameObject btn_prefab_;
    [SerializeField]
    private Transform btn_parent_;

    private int cur_idx_ = 0;
    private List<ScenarioSlot> scenarior_list_ = new List<ScenarioSlot>();

    public void init()
    {
        var scenario_arr = ScenarioDataBase.instance.scenario_arr;

        for (int i = 0; i < scenario_arr.Length; i++)
        {
            var go = GameObject.Instantiate(btn_prefab_);
            go.transform.SetParent(btn_parent_);
            var cp = go.GetComponent<ScenarioSlot>();
            cp.setText(scenario_arr[i].scenario_name);
            cp.setNo(i);
            if (i == cur_idx_)
            {
                cp.GetComponent<Image>().color = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
            }
            else
            {
                cp.GetComponent<Image>().color = Color.white;
            }
            scenarior_list_.Add(cp);
        }
    }

    public void selectButton(int _idx)
    {
        foreach (var cp in scenarior_list_)
        {
            if (cp.no == _idx)
            {
                cp.GetComponent<Image>().color = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
            }
            else
            {
                cp.GetComponent<Image>().color = Color.white;
            }
        }
        cur_idx_ = _idx;
    }

    public void onClickImportButton()
    {
        SceneDataManager.instance.setScenario(ScenarioDataBase.instance.scenario_arr[cur_idx_]);
        SceneManager.LoadScene("SetupScene");
    }
}
