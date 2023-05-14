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
    private ScheduleInfoDisplay schedule_info_ui_;
    public ScheduleInfoDisplay schedule_info_ui { get => schedule_info_ui_; }

    [SerializeField]
    private ResultTable result_table_ui_;
    public ResultTable resut_table_ui { get => result_table_ui_; }

    [SerializeField]
    private ChartSwapButton swap_button_ui_;
    public ChartSwapButton swap_button_ui { get => swap_button_ui_; }

    public void init()
    {
        processor_chart_ui_.init();
        job_table_ui_.init();
        finish_process_table_ui_.init();
        schedule_info_ui_.init();
    }
}
