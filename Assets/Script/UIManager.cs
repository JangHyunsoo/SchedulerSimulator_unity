using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private ProcessorChart processor_chart_ui_;
    public ProcessorChart processor_chart_ui { get => processor_chart_ui_; }

    [SerializeField]
    private FinishProcessTable finish_process_table_ui_;
    public FinishProcessTable finish_process_table_ui { get => finish_process_table_ui_; }

    public void init()
    {
        processor_chart_ui_.init();
        finish_process_table_ui_.init();
    }
}
