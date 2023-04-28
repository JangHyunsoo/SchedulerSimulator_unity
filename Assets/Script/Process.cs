using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process
{
	private int process_no_;
	private int arrival_time_;
	private int burst_time_;
	private int cur_burst_time_;
	private int remaining_time_;

	public int no { get => process_no_; }
	public int arrival_time { get => arrival_time_; }
	public int burst_time { get => burst_time_; }
	public int cur_burst_time { get => cur_burst_time_; }
	public bool is_dead { get => cur_burst_time_ <= 0; }

	public Process(Job _job)
	{
		process_no_ = _job.job_no;
		arrival_time_ = _job.arrival_time;
		burst_time_ = _job.brust_time;
		cur_burst_time_ = _job.brust_time;
		remaining_time_ = 0;
	}

	public int getWaitingTime(int _total_tick)
	{
		// TT = _total_tick - arrival_time_
		return (_total_tick - arrival_time_ + burst_time) / burst_time;
	}

	public void tick(int work)
	{
		cur_burst_time_ -= work;
		remaining_time_ -= 1; // remaining_time -= work? or remaining_time -= 1? -> processor memeber?
		if (cur_burst_time_ < 0)
		{
			cur_burst_time_ = 0;
		}
		if (remaining_time_ < 0)
		{
			remaining_time_ = 0;
		}
	}
}
