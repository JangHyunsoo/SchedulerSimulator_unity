using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorChart : MonoBehaviour
{
    [Header("Process")]
    [SerializeField]
    private GameObject chart_process_unit_prefab_;
    [SerializeField]
    private Transform chart_process_unit_parent_;
    [SerializeField]
    private RectTransform chart_process_rect_transform_;
    [SerializeField]
    private ScrollRect chart_process_scroll_rect_;
    [SerializeField]
    private GridLayoutGroup chart_process_grid_;
    [SerializeField]
    private RectTransform chart_process_scroll_rect_transform_;

    [Header("Processor")]
    [SerializeField]
    private GameObject chart_p_processor_unit_prefab_;
    [SerializeField]
    private GameObject chart_e_processor_unit_prefab_;
    [SerializeField]
    private Transform chart_processor_unit_parent_;
    [SerializeField]
    private RectTransform chart_processor_rect_transform_;
    [SerializeField]
    private ScrollRect chart_procesesor_scroll_rect_;
    [SerializeField]
    private GridLayoutGroup chart_processor_grid_;
    [SerializeField]
    private RectTransform chart_processor_scroll_rect_transform_;

    [Header("Background")]
    [SerializeField]
    private GameObject chart_background_unit_prefab_;
    [SerializeField]
    private Transform chart_background_unit_parent_;
    [SerializeField]
    private RectTransform chart_background_rect_transform_;
    [SerializeField]
    private ScrollRect chart_background_scroll_rect_;
    [SerializeField]
    private GridLayoutGroup chart_background_grid_;

    [Header("Tick")]
    [SerializeField]
    private GameObject chart_tick_unit_prefab_;
    [SerializeField]
    private GameObject first_chart_tick_unit_prefab_;
    [SerializeField]
    private Transform chart_tick_unit_parent_;
    [SerializeField]
    private RectTransform chart_tick_rect_transfrom_;
    [SerializeField]
    private ScrollRect chart_tick_scroll_rect_;
    [SerializeField]
    private GridLayoutGroup chart_tick_grid_;
    [SerializeField]
    private RectTransform rect_tick_;

    [SerializeField]
    private Color start_color;
    [SerializeField]
    private Color target_color;

    private Dictionary<int, Color> chart_color_table = new Dictionary<int, Color>();
    private List<Transform> chart_unit_queue_ = new List<Transform>();
    private List<Transform> tick_list_ = new List<Transform>();
    private int processor_size;

    private bool height_auto_ = false;
    private bool width_auto_ = false;

    public void init()
    {
        processor_size = ProcessorManager.instance.processor_count;
        chart_color_table.Clear();
        chart_unit_queue_.Clear();

        int p_core_count = ProcessorManager.instance.p_core_count;
        int e_core_count = ProcessorManager.instance.e_core_count;

        for (int i = 0; i < processor_size; i++)
        {
            Color color = Color.Lerp(start_color, target_color, 1f / processor_size * i);
            if(p_core_count > 0)
            {
                addProcessor(ProcessorType.PERFOR ,color);
                p_core_count--;
            }
            else
            {
                addProcessor(ProcessorType.EFFIC, color);
            }
        }

        if(processor_size < 4) 
        {
            rect_tick_.position = new Vector2(rect_tick_.position.x, rect_tick_.position.y + 84 * (4 - processor_size));
        }

        for (int i = 0;i < 21; i++)
        {
            addTick(100);
        }

        autoHeightSize();
    }

    public void autoHeightSize()
    {
        if (!height_auto_ || processor_size <= 4)
        {
            chart_process_grid_.spacing = new Vector2(chart_process_grid_.spacing.x, 4);
            chart_processor_grid_.spacing = new Vector2(0, 4);
            chart_background_grid_.spacing = new Vector2(0, 4);

            chart_process_grid_.cellSize = new Vector2(chart_process_grid_.cellSize.x, 80);
            chart_processor_grid_.cellSize = new Vector2(chart_processor_grid_.cellSize.x, 80);
            chart_background_grid_.cellSize = new Vector2(chart_background_grid_.cellSize.x, 80);

            float rect_height = processor_size * (80 + 4);
            var process_rect = chart_process_rect_transform_.rect;

            chart_process_rect_transform_.sizeDelta = new Vector2(chart_process_rect_transform_.sizeDelta.x, rect_height);
            chart_processor_rect_transform_.sizeDelta = new Vector2(chart_processor_rect_transform_.sizeDelta.x, rect_height);
            chart_background_rect_transform_.sizeDelta = new Vector2(chart_background_rect_transform_.sizeDelta.x, rect_height);
        }
        else
        {
            chart_process_grid_.spacing = new Vector2(chart_process_grid_.spacing.x, 0);
            chart_processor_grid_.spacing = new Vector2(0, 0);
            chart_background_grid_.spacing = new Vector2(0, 0);

            float process_height_size = chart_processor_scroll_rect_transform_.sizeDelta.y / processor_size;
            chart_process_grid_.cellSize = new Vector2(chart_process_grid_.cellSize.x, process_height_size);
            chart_processor_grid_.cellSize = new Vector2(chart_processor_grid_.cellSize.x, process_height_size);
            chart_background_grid_.cellSize = new Vector2(chart_background_grid_.cellSize.x, process_height_size);

            float rect_height = processor_size * process_height_size;

            chart_process_rect_transform_.sizeDelta = new Vector2(chart_process_rect_transform_.sizeDelta.x, rect_height);
            chart_processor_rect_transform_.sizeDelta = new Vector2(chart_processor_rect_transform_.sizeDelta.x, rect_height);
            chart_background_rect_transform_.sizeDelta = new Vector2(chart_background_rect_transform_.sizeDelta.x, rect_height);
        }
    }

    public void addChartUnit(Process _history)
    {
        GameObject go = GameObject.Instantiate(chart_process_unit_prefab_);
        var cu = go.GetComponent<ChartUnit>();
        if (_history != null)
        {
            cu.setColor(JobSimulator.instance.getProcessColor(_history.no));
        }
        else
        {
            cu.setColor(JobSimulator.instance.getProcessColor(-1));
        }
        cu.setProcess(_history);
        go.transform.SetParent(chart_process_unit_parent_);
        chart_unit_queue_.Add(go.transform);
    }

    public void addProcessor(ProcessorType type, Color color)
    {
        GameObject processor_unit_prefab = type == ProcessorType.PERFOR ? chart_p_processor_unit_prefab_ : chart_e_processor_unit_prefab_;
        var go1 = GameObject.Instantiate(processor_unit_prefab);
        go1.transform.SetParent(chart_processor_unit_parent_);
        var go2 = GameObject.Instantiate(chart_background_unit_prefab_);
        go2.transform.SetParent(chart_background_unit_parent_);
        go2.GetComponent<Image>().color = color;
    }

    public void addTick(int _total_tick = 20)
    {
        if (_total_tick >= tick_list_.Count)
        {
            GameObject tick_prefab = tick_list_.Count == 0 ? first_chart_tick_unit_prefab_ : chart_tick_unit_prefab_;
            GameObject go = GameObject.Instantiate(tick_prefab);
            go.transform.SetParent(chart_tick_unit_parent_);
            go.GetComponentInChildren<Text>().text = (tick_list_.Count + 1).ToString();
            tick_list_.Add(go.transform);
        }
    }

    public void setAutoWidthSize()
    {
        if (width_auto_ && tick_list_.Count >= 22)
        {
            chart_process_grid_.spacing = new Vector2(0, chart_process_grid_.spacing.y);
            chart_tick_grid_.spacing = new Vector2(0, 0);

            chart_process_rect_transform_.sizeDelta = new Vector2(chart_process_scroll_rect_transform_.sizeDelta.x, chart_process_rect_transform_.sizeDelta.y);
            chart_tick_rect_transfrom_.sizeDelta = new Vector2(chart_process_scroll_rect_transform_.sizeDelta.x, chart_tick_rect_transfrom_.sizeDelta.y);
        }
        else
        {
            chart_process_grid_.spacing = new Vector2(4, chart_process_grid_.spacing.y);
            chart_tick_grid_.spacing = new Vector2(4, 0);

            chart_process_grid_.cellSize = new Vector2(80, chart_process_grid_.cellSize.y);
            chart_tick_grid_.cellSize = new Vector2(80, chart_tick_grid_.cellSize.y);
        }
    }

    public void autoWidthSize()
    {
        if (width_auto_ && tick_list_.Count >= 22)
        {
            if (chart_process_grid_.spacing.x != 0)
            {
                setAutoWidthSize();
            }

            float grid_cell_x = chart_process_rect_transform_.sizeDelta.x / tick_list_.Count;

            chart_process_grid_.cellSize = new Vector2(grid_cell_x, chart_process_grid_.cellSize.y);
            chart_tick_grid_.cellSize = new Vector2(grid_cell_x, chart_tick_grid_.cellSize.y);
        }
        else
        {
            if(chart_process_grid_.spacing.x == 0)
            {
                setAutoWidthSize();
            }

            float rect_width = tick_list_.Count * (80 + 4);

            chart_process_rect_transform_.sizeDelta = new Vector2(rect_width, chart_process_rect_transform_.sizeDelta.y);
            chart_tick_rect_transfrom_.sizeDelta = new Vector2(rect_width, chart_tick_rect_transfrom_.sizeDelta.y);
        }


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
        chart_background_scroll_rect_.verticalNormalizedPosition = vector2.y;
    }

    public void toggleHeightAuto()
    {
        height_auto_ = !height_auto_;
        autoHeightSize();
    }

    public void toggleWidthAuto()
    {
        width_auto_ = !width_auto_;
        autoWidthSize();
    }
}
