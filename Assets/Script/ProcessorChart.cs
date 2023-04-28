using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessorChart : MonoBehaviour
{
    [SerializeField]
    private GameObject chart_unit_prefab_;
    [SerializeField]
    private Transform chart_unit_parent_;

    private Dictionary<int, Color> chart_color_table = new Dictionary<int, Color>();
    private Queue<Transform> chart_unit_queue_ = new Queue<Transform>();
    private int processor_size = 4;

    public void init()
    {
        processor_size = ProcessorManager.instance.processor_count;
        chart_color_table.Clear();
        chart_unit_queue_.Clear();
        chart_color_table[-1] = Color.clear;
    }

    public void addChartUnit(int _history)
    {
        GameObject go = GameObject.Instantiate(chart_unit_prefab_);
        if (chart_color_table.ContainsKey(_history))
        {
            chart_color_table[_history] = new Color(Random.RandomRange(0, 255), Random.RandomRange(0, 255), Random.RandomRange(0, 255));
        }
        go.GetComponent<ChartUnit>().setColor(chart_color_table[_history]);
        go.transform.SetParent(chart_unit_parent_);
        go.active = false;
        chart_unit_queue_.Enqueue(go.transform);
    }

    public void assginChartUnit(List<int> _progress_history)
    {
        foreach (var progress_info in _progress_history)
        {
            addChartUnit(progress_info);
        }
    }

    public void stepChart()
    {
        int cur_count = processor_size;

        while (chart_unit_queue_.Count != 0 && cur_count != 0)
        {
            chart_unit_queue_.Dequeue().gameObject.active = true;
            cur_count--;
        }
    }

    public void jumpChart()
    {
        while (chart_unit_queue_.Count != 0)
        {
            chart_unit_queue_.Dequeue().gameObject.active = true;
        }
    }
}
