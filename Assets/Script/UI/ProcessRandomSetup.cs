using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessRandomSetup : MonoBehaviour
{
    [SerializeField]
    private InputField process_count_text_;
    [SerializeField]
    private InputField bt_min_text_;
    [SerializeField]
    private InputField bt_max_text_;
    [SerializeField]
    private InputField at_min_text_;
    [SerializeField]
    private InputField at_max_text_;

    // add process
    public void addRandomJob()
    {
        List<Job> job_list = new List<Job>();
        if (checkInputField())
        {
            int process_count = int.Parse(process_count_text_.text);
            int bt_min = int.Parse(bt_min_text_.text);
            int bt_max = int.Parse(bt_max_text_.text);
            int at_min = int.Parse(at_min_text_.text);
            int at_max = int.Parse(at_max_text_.text);

            for (int i = 0; i < process_count; i++)
            {
                int bt = Random.Range(bt_min, bt_max + 1);
                int at = Random.Range(at_min, at_max + 1);
                SetUpManager.instance.addJob(at, bt);
            }
        }
    }

    private bool checkInputField()
    {
        if (process_count_text_.text == "" || process_count_text_.text == "0") return false;
        if (bt_min_text_.text == "" || bt_max_text_.text == "") return false;
        else if (int.Parse(bt_min_text_.text) > int.Parse(bt_max_text_.text)) return false;
        if (at_min_text_.text == "" || at_min_text_.text == "") return false;
        else if (int.Parse(at_min_text_.text) > int.Parse(at_max_text_.text)) return false;
        return true;
    }

    // change input field
    public void changeProcessCount(string _num)
    {
        process_count_text_.text = underZero(_num);
    }

    public void changeATMax(string _num)
    {
        at_max_text_.text = underZero(_num);
    }

    public void changeATMin(string _num)
    {
        at_min_text_.text = underZero(_num);
    }

    public void changeBTMax(string _num)
    {
        if (_num.Contains("-") || _num == "" || _num == "0")
        {
            bt_max_text_.text = "1";
        }
        else
        {
            bt_max_text_.text = _num;
        }
    }

    public void changeBTMin(string _num)
    {
        if(_num == "")
        {
            bt_min_text_.text = "1";
        }
        else if(int.Parse(_num) <= 0)
        {
            bt_min_text_.text = "1";
        }
        else
        {
            bt_min_text_.text = _num;
        }
    }

    private string underZero(string _num)
    {
        if (_num == "") return "0";
        else if (int.Parse(_num) < 0) return "0";
        return _num;
    }

    // increase
    public void increaseProcessCount()
    {
        int count = int.Parse(process_count_text_.text) + 1;

        if(count >= 1000)
        {
            process_count_text_.text = "999";
        }
        else
        {
            process_count_text_.text = count.ToString();
        }
    }

    public void increaseATMax()
    {
        int count = int.Parse(at_max_text_.text) + 1;
        if (count >= 1000)
        {
            at_max_text_.text = "999";
        }
        else
        {
            at_max_text_.text = count.ToString();
        }
    }

    public void increaseATMin()
    {
        int count = int.Parse(at_min_text_.text) + 1;
        if (count >= 1000)
        {
            at_min_text_.text = "999";
        }
        else
        {
            at_min_text_.text = count.ToString();
        }
    }

    public void increaseBTMax()
    {
        int count = int.Parse(bt_max_text_.text) + 1;
        if (count >= 1000)
        {
            bt_max_text_.text = "999";
        }
        else
        {
            bt_max_text_.text = count.ToString();
        }

    }

    public void increaseBTMin()
    {
        int count = int.Parse(bt_min_text_.text) + 1;
        if (count >= 1000)
        {
            bt_min_text_.text = "999";
        }
        else
        {
            bt_min_text_.text = count.ToString();
        }
    }

    // reduce
    public void reduceProcessCount()
    {
        int count = int.Parse(process_count_text_.text) - 1;
        if (count < 1)
        {
            process_count_text_.text = "1";
        }
        else
        {
            process_count_text_.text = count.ToString();
        }
    }

    public void reduceATMax()
    {
        int count = int.Parse(at_max_text_.text) - 1;
        if (count < 0)
        {
            at_max_text_.text = "0";
        }
        else
        {
            at_max_text_.text = count.ToString();
        }
    }

    public void reduceATMin()
    {
        int count = int.Parse(at_min_text_.text) - 1;
        if (count < 0)
        {
            at_min_text_.text = "0";
        }
        else
        {
            at_min_text_.text = count.ToString();
        }
    }

    public void reduceBTMax()
    {
        int count = int.Parse(bt_max_text_.text) - 1;
        if (count < 1)
        {
            bt_max_text_.text = "1";
        }
        else
        {
            bt_max_text_.text = count.ToString();
        }
    }

    public void reduceBTMin()
    {
        int count = int.Parse(bt_min_text_.text) - 1;
        if (count < 1)
        {
            bt_min_text_.text = "1";
        }
        else
        {
            bt_min_text_.text = count.ToString();
        }
    }
}
