using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process
{
	private int process_no_;
	private int arrival_time_;
	private int burst_time_;
	private int response_time_;
	private int cur_burst_time_;
	private int end_time_;
	private int remain_quantum_time_;
	private float response_ratio_;

	public int no { get => process_no_; }
	public int arrival_time { get => arrival_time_; }
	public int burst_time { get => burst_time_; }
	public int cur_burst_time { get => cur_burst_time_; }
	public int waiting_time { get => response_time_ - arrival_time_; }
	public int total_time { get => end_time_ - arrival_time_; }
	public float normalized_total_time { get => (float)total_time / (float)burst_time; }
	public float response_ratio { get => response_ratio_; }
	public bool end_quantum { get => remain_quantum_time_ <= 0; }
	public bool is_dead { get => cur_burst_time_ <= 0; }

	public Process(Job _job)
	{
		process_no_ = _job.job_no;
		arrival_time_ = _job.arrival_time;
		burst_time_ = _job.brust_time;
		cur_burst_time_ = _job.brust_time;
		remain_quantum_time_ = 0;
		response_ratio_ = 0f;
		response_time_ = -1;
	}

	public void setQuantumTime(int t)
    {
		remain_quantum_time_ = t;
    }

	public void setEndTime(int t)
    {
		end_time_ = t;
    }

	public void setResponseRatio(int _total_tick)
    {
		response_ratio_ = (float)(_total_tick - arrival_time_ + burst_time) / (float)burst_time;

	}

	public void tick(int _total_tick, int work)
	{
		if(response_time_ == -1)
        {
			response_time_ = _total_tick + 1;
        }
		cur_burst_time_ -= work;
		remain_quantum_time_ -= 1;
		if (cur_burst_time_ < 0)
		{
			cur_burst_time_ = 0;
		}
		if (remain_quantum_time_ < 0)
		{
			remain_quantum_time_ = 0;
		}
	}
}
