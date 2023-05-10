using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JobSimulator : Singleton<JobSimulator>
{
    private Job[] job_arr_;
    public Job[] job_arr { get => job_arr_; }
    private Queue<Job> job_queue_ = new Queue<Job>();

    public int job_size { get => job_arr_.Length; }
    public bool is_empty { get => job_queue_.Count == 0; }
    public bool is_done { get => is_empty; }

    private Dictionary<int, Color> color_table = new Dictionary<int, Color>();

    public Color getProcessColor(int no)
    {
        return color_table[no];
    }

    public void init()
    {
        job_queue_.Clear();
        
        // load data
        job_arr_ = SceneDataManager.instance.getJobs().Clone() as Job[];

        var job_color_arr = SceneDataManager.instance.getJobColors();

        for (int i = 0; i < job_color_arr.Length; i++)
        {
            color_table[i] = job_color_arr[i];
        }

        color_table[-1] = Color.clear;
        
        carryQueue();
    }

    private void carryQueue()
    {
        Array.Sort(job_arr_, delegate(Job one, Job other) {
            if (one.arrival_time == other.arrival_time)
            {
                if (one.job_no == other.job_no) return 0;
                else if (one.job_no < other.job_no) return 1;
                else return -1;
            }
            if (one.arrival_time > other.arrival_time) return 1;
            else return -1;
        });
        foreach (Job job in job_arr_)
        {
            job_queue_.Enqueue(job);
        }
    }

    public Queue<Job> getJobs(int _total_tick)
    {
        Queue<Job> ret = new Queue<Job>();

        if (job_queue_.Count == 0) return ret;

        while (job_queue_.Count != 0)
        {
            if (job_queue_.Peek().arrival_time == _total_tick)
            {
                ret.Enqueue(job_queue_.Dequeue());
            }
            else break;
        }
        return ret;
    }
}