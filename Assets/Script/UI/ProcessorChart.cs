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

    [SerializeField]
    private Color start_color;
    [SerializeField]
    private Color target_color;

    private Dictionary<int, Color> chart_color_table = new Dictionary<int, Color>();
    private List<Transform> chart_unit_queue_ = new List<Transform>();
    private List<Transform> tick_list_ = new List<Transform>();
    private int processor_size = 4;

    public void init()
    {
        processor_size = ProcessorManager.instance.processor_count;
        chart_color_table.Clear();
        chart_unit_queue_.Clear();

        for (int i = 0; i < processor_size; i++)
        {
            Color color = Color.Lerp(start_color, target_color, 1f / processor_size * i);
            addProcessor(color);
        }

        if (processor_size > 4)
        {
            float rect_height = processor_size * (80 + 4) + 2;
            var process_rect = chart_process_rect_transform_.rect;

            chart_process_rect_transform_.sizeDelta = new Vector2(chart_process_rect_transform_.sizeDelta.x, rect_height);
            chart_processor_rect_transform_.sizeDelta = new Vector2(chart_processor_rect_transform_.sizeDelta.x, rect_height);

            //chart_process_rect_transform_.gameObject.GetComponent<GridLayoutGroup>().
        }
    }

    public void addChartUnit(int _history)
    {
        GameObject go = GameObject.Instantiate(chart_process_unit_prefab_);
        go.GetComponent<ChartUnit>().setColor(JobSimulator.instance.getProcessColor(_history));
        go.transform.SetParent(chart_process_unit_parent_);
        chart_unit_queue_.Add(go.transform);
    }

    public void addProcessor(Color color)
    {
        GameObject go = GameObject.Instantiate(chart_processor_unit_prefab_);
        go.transform.SetParent(chart_processor_unit_parent_);
        go.GetComponent<ChartProcessorUnit>().setColor(color);
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
        go.GetComponentInChildren<Text>().text = tick_list_.Count.ToString();
        tick_list_.Add(go.transform);
    }

    public void autoSize()
    {
        float rect_width = tick_list_.Count * (80 + 4) + 2;
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

    public void syncProcessorScroll(Vector2 vector2)
    {
        chart_procesesor_scroll_rect_.verticalNormalizedPosition = vector2.y;
    }
}
