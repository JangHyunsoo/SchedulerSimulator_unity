using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorChart : MonoBehaviour
{
    [SerializeField]
    private GameObject chart_process_unit_prefab_;
    [SerializeField]
    private Transform chart_process_unit_parent_;
    [SerializeField]
    private RectTransform chart_process_rect_transform_;
    [SerializeField]
    private ScrollRect chart_process_scroll_rect_;

    [SerializeField]
    private GameObject chart_processor_unit_prefab_;
    [SerializeField]
    private Transform chart_processor_unit_parent_;
    [SerializeField]
    private RectTransform chart_processor_rect_transform_;
    [SerializeField]
    private ScrollRect chart_procesesor_scroll_rect_;

    [SerializeField]
    private GameObject chart_tick_unit_prefab_;
    [SerializeField]
    private Transform chart_tick_unit_parent_;
    [SerializeField]
    private RectTransform chart_tick_rect_transfrom_;
    [SerializeField]
    private ScrollRect chart_tick_scroll_rect_;


    private Dictionary<int, Color> chart_color_table = new Dictionary<int, Color>();
    private List<Transform> chart_unit_queue_ = new List<Transform>();
    private List<Transform> tick_list_ = new List<Transform>();
    private int processor_size = 4;

    public void init()
    {
        processor_size = ProcessorManager.instance.processor_count;
        chart_color_table.Clear();
        chart_unit_queue_.Clear();

        if (processor_size > 4)
        {
            var process_rect = chart_process_scroll_rect_.content.sizeDelta;
            float n_height = process_rect.y * 4f / processor_size;
            chart_process_scroll_rect_.content.sizeDelta = new Vector2(process_rect.x, n_height);
        }
    }

    public void addChartUnit(int _history)
    {
        GameObject go = GameObject.Instantiate(chart_process_unit_prefab_);
        go.GetComponent<ChartUnit>().setColor(JobSimulator.instance.getProcessColor(_history));
        go.transform.SetParent(chart_process_unit_parent_);
        chart_unit_queue_.Add(go.transform);
    }

    public void assginChartUnit(List<int> _progress_history)
    {
        foreach (var progress_info in _progress_history)
        {
            addChartUnit(progress_info);
        }
        addTick();
    }

    public void addTick()
    {
        GameObject go = GameObject.Instantiate(chart_tick_unit_prefab_);
        go.transform.SetParent(chart_tick_unit_parent_);
        tick_list_.Add(go.transform);
    }

    public void autoSize()
    {
        float rect_width = tick_list_.Count * (80 + 2) + 2;
        var process_rect = chart_process_rect_transform_.rect;

        chart_process_rect_transform_.sizeDelta = new Vector2(rect_width, chart_process_rect_transform_.sizeDelta.y);
        chart_tick_rect_transfrom_.sizeDelta = new Vector2(rect_width, chart_tick_rect_transfrom_.sizeDelta.y);

        chart_process_scroll_rect_.horizontalNormalizedPosition = 1f;
        chart_tick_scroll_rect_.horizontalNormalizedPosition = 1f;
    }

    public void syncTickScroll(Vector2 vector2)
    {
        chart_tick_scroll_rect_.horizontalNormalizedPosition = vector2.x;
    }
}
