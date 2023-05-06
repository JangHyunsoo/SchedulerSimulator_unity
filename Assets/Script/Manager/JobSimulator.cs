using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JobSimulator : Singleton<JobSimulator>
{
    private int job_counter_ = 0;
    private Job[] job_arr_;
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
        job_arr_ = new Job[30];
        job_queue_.Clear();

        color_table[-1] = Color.clear;

        for (int i = 0; i < 30; i++)
        {
            addJobByRandom();
            color_table[i] = new Color(Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f));
        }
        carryQueue();
    }

    private void addJobByRandom()
    {
        int arrival_time = Random.Range(0, 20);
        int burst_time = Random.Range(1, 20);
        addJob(arrival_time, burst_time);
    }

    private void addJob(int _arrival_time, int _burst_time)
    {
        job_arr_[job_counter_] = new Job { job_no = job_counter_, arrival_time = _arrival_time, brust_time = _burst_time };
        job_counter_++;
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
            if (job_queue_.Peek().arrival_time <= _total_tick)
            {
                ret.Enqueue(job_queue_.Dequeue());
            }
            else break;
        }
        return ret;
    }
}