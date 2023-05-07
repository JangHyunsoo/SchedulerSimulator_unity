using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private ProcessorChart processor_chart_ui_;
    public ProcessorChart processor_chart_ui { get => processor_chart_ui_; }

    [SerializeField]
    private ChartProcessQueueUI chart_process_queue_ui_;
    public ChartProcessQueueUI chart_process_queue_ui { get => chart_process_queue_ui_; }

    [SerializeField]
    private JobTable job_table_ui_;
    public JobTable job_table_ui { get => job_table_ui_; }

    [SerializeField]
    private FinishProcessTable finish_process_table_ui_;
    public FinishProcessTable finish_process_table_ui { get => finish_process_table_ui_; }

    [SerializeField]
    private Text power_text_;
    public Text power_text { get => power_text_; }

    public void init()
    {
        processor_chart_ui_.init();
        job_table_ui_.init();
        finish_process_table_ui_.init();
    }
}
