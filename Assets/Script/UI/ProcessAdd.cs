using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessAdd : MonoBehaviour
{
    [SerializeField]
    private InputField arrival_time_text_;

    [SerializeField]
    private InputField burst_time_text_;

    [SerializeField]
    private Image color_btn_image_;

    [SerializeField]
    private Transform start_add_btn_;

    [SerializeField]
    private Transform add_field_;

    private Color color_;

    public void init()
    {
        arrival_time_text_.text = "0";
        burst_time_text_.text = "1";
        changeColor();
    }

    public void changeArrivalTime(string _num)
    {
        if(_num.Contains("-") || _num == "")
        {
            arrival_time_text_.text = "0";
        }
        else
        {
            arrival_time_text_.text = _num;
        }
    }

    public void changeBurstTime(string _num)
    {
        if (_num.Contains("-") || _num == "" || _num == "0")
        {
            burst_time_text_.text = "1";
        }
        else
        {
            burst_time_text_.text = _num;
        }

    }

    public void changeColor()
    {
        color_ = new Color(Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f));
        color_btn_image_.GetComponent<Image>().color = color_;
    }

    public void increaseArrivalTime()
    {
        int arrival_time = int.Parse(arrival_time_text_.text) + 1;
        if (arrival_time >= 1000)
        {
            arrival_time_text_.text = "999";
        }
        else
        {
            arrival_time_text_.text = arrival_time.ToString();
        }
    }

    public void decreaseArrivalTime()
    {
        int arrival_time = int.Parse(arrival_time_text_.text) - 1;
        if (arrival_time < 0)
        {
            arrival_time_text_.text = "0";
        }
        else
        {
            arrival_time_text_.text = arrival_time.ToString();
        }
    }

    public void increaseBurstTime()
    {
        int burst_time = int.Parse(burst_time_text_.text) + 1;
        if (burst_time >= 1000)
        {
            burst_time_text_.text = "999";
        }
        else
        {
            burst_time_text_.text = burst_time.ToString();
        }
    }

    public void decreaseBurstTime()
    {
        int burst_time = int.Parse(burst_time_text_.text) - 1;
        if (burst_time <= 1)
        {
            burst_time_text_.text = "1";
        }
        else
        {
            burst_time_text_.text = burst_time.ToString();
        }
    }

    public void startAdd()
    {
        init();
        start_add_btn_.gameObject.SetActive(false);
        add_field_.gameObject.SetActive(true);
    }

    public void add()
    {
        SetUpManager.instance.addJob(color_, int.Parse(arrival_time_text_.text), int.Parse(burst_time_text_.text));
        start_add_btn_.gameObject.SetActive(true);
        add_field_.gameObject.SetActive(false);
    }

}
