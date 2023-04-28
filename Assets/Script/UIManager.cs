using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private CPUPanelUI cpu_panel_ui_;
    public CPUPanelUI cup_panel_ui { get => cpu_panel_ui_; }

    [SerializeField]
    private ProcessorChart processor_chart_ui_;
    public ProcessorChart processor_chart_ui { get => processor_chart_ui_; }

    public void init()
    {
        cpu_panel_ui_.init();
        processor_chart_ui_.init();
    }
}
