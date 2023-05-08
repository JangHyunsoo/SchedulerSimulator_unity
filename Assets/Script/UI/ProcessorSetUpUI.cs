using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorSetUpUI : MonoBehaviour
{
    [SerializeField]
    private Transform contents_parents_;

    [SerializeField]
    private GameObject p_core_prefab_;

    [SerializeField]
    private GameObject e_core_prefab_;

    [SerializeField]
    private Text p_core_text_;

    [SerializeField]
    private Text e_core_text_;

    public void init()
    {
        int p_core_count = SetUpManager.instance.p_core_count;
        int e_core_count = SetUpManager.instance.e_core_count;

        for (int i = 0; i < p_core_count; i++)
        {
            var go = GameObject.Instantiate(p_core_prefab_);
            go.transform.SetParent(contents_parents_);
        }

        for (int i = 0; i < e_core_count; i++)
        {
            var go = GameObject.Instantiate(e_core_prefab_);
            go.transform.SetParent(contents_parents_);
        }

        p_core_text_.text = p_core_count.ToString();
        e_core_text_.text = e_core_count.ToString();
    }

    public void addPCore()
    {
        SetUpManager.instance.increasePCore();
        var go = GameObject.Instantiate(p_core_prefab_);
        go.transform.SetParent(contents_parents_);

        p_core_text_.text = SetUpManager.instance.p_core_count.ToString();
    }

    public void addEcore()
    {
        SetUpManager.instance.increaseECore();
        var go = GameObject.Instantiate(e_core_prefab_);
        go.transform.SetParent(contents_parents_);

        e_core_text_.text = SetUpManager.instance.e_core_count.ToString();
    }

    public void deletePcore()
    {
        p_core_text_.text = SetUpManager.instance.p_core_count.ToString();
    }

    public void deleteEcore()
    {
        e_core_text_.text = SetUpManager.instance.e_core_count.ToString();
    }
}
