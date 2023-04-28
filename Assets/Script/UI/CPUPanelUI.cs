using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUPanelUI : MonoBehaviour
{
    [SerializeField]
    private GameObject cpu_info_ui_prefab_;

    [SerializeField]
    private Transform cpu_info_ui_parent_;
    [SerializeField]
    private CPUInfoUI[] cpu_info_ui_arr_;

    [SerializeField]
    private RectTransform cpu_parent_rect_tr_;
    [SerializeField]
    private Transform cpu_panel_scroll_;

    public void init()
    {
        var psr_mgr = ProcessorManager.instance;
        var psr_count = psr_mgr.processor_count;

        for (int i = 0; i < psr_count; i++)
        {
            addProcessor(psr_mgr.getProcessor(i));
        }

        cpu_info_ui_arr_ = cpu_info_ui_parent_.GetComponentsInChildren<CPUInfoUI>();
        autoSize();
    }

    public void addProcessor(Processor processor)
    {
        GameObject go = GameObject.Instantiate(cpu_info_ui_prefab_);
        CPUInfoUI cpu_info_ui = go.GetComponent<CPUInfoUI>();
        cpu_info_ui.setProcessor(processor);
        go.transform.SetParent(cpu_info_ui_parent_);
    }

    public void autoSize()
    {
        float rect_width = cpu_info_ui_arr_.Length * (175 + 20) + 20;
        var rect = cpu_parent_rect_tr_.rect;

        cpu_parent_rect_tr_.sizeDelta = new Vector2(rect_width, rect.height);
        cpu_panel_scroll_.gameObject.active = cpu_info_ui_arr_.Length != 0;
    }
}
