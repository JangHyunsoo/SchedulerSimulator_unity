using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorChart : MonoBehaviour
{
    [SerializeField]
    private GameObject chart_unit_prefab_;
    [SerializeField]
    private Transform chart_unit_parent_;
    [SerializeField]
    private RectTransform chart_rect_transform_;
    [SerializeField]
    private ScrollRect char_scroll_rect_;

    private Dictionary<int, Color> chart_color_table = new Dictionary<int, Color>();
    private Queue<Transform> chart_unit_queue_ = new Queue<Transform>();
    private int processor_size = 4;

    public void init()
    {
        processor_size = ProcessorManager.instance.processor_count;
        chart_color_table.Clear();
        chart_unit_queue_.Clear();

        if (processor_size > 4)
        {
            var rect = char_scroll_rect_.content.sizeDelta;
            float n_height = rect.y * 4f / processor_size;
            char_scroll_rect_.content.sizeDelta = new Vector2(rect.x, n_height);
        }
    }

    public void addChartUnit(int _history)
    {
        GameObject go = GameObject.Instantiate(chart_unit_prefab_);
        go.GetComponent<ChartUnit>().setColor(JobSimulator.instance.getProcessColor(_history));
        go.transform.SetParent(chart_unit_parent_);
        chart_unit_queue_.Enqueue(go.transform);
    }

    public void assginChartUnit(List<int> _progress_history)
    {
        foreach (var progress_info in _progress_history)
        {
            addChartUnit(progress_info);
        }
    }

    public void autoSize()
    {
        float rect_width = chart_unit_queue_.Count / processor_size * char_scroll_rect_.content.rect.width;
        var rect = chart_rect_transform_.rect;

        chart_rect_transform_.sizeDelta = new Vector2(rect_width, chart_rect_transform_.sizeDelta.y);

        char_scroll_rect_.horizontalNormalizedPosition = 1f;
    }
}
