using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process
{
	private int process_no_;
	private int arrival_time_;
	private int burst_time_;
	private int response_time_;
	private int remaining_time_;
	private int end_time_;
	private float response_ratio_;

	public int no { get => process_no_; }
	public int arrival_time { get => arrival_time_; }
	public int burst_time { get => burst_time_; }
	public int remaining_time { get => remaining_time_; }
	public int waiting_time { get => response_time_ - arrival_time_; }
	public int turn_around_time { get => end_time_ - arrival_time_; }
	public float normalized_turn_around_time { get => (float)turn_around_time / (float)burst_time; }
	public float response_ratio { get => response_ratio_; }
	public bool is_terminaled { get => remaining_time_ <= 0; }

	public Process()
    {
		process_no_ = 0;
		arrival_time_ = 0;
		burst_time_ = 0;
		remaining_time_ = 0;
		response_ratio_ = 0f;
		response_time_ = -1;
	}

	public Process(int _no, int _burst_time)
	{
		process_no_ = _no;
		arrival_time_ = 0;
		burst_time_ = _burst_time;
		remaining_time_ = burst_time;
		response_ratio_ = 0f;
		response_time_ = -1;
	}

	public Process(Job _job)
	{
		process_no_ = _job.job_no;
		arrival_time_ = _job.arrival_time;
		burst_time_ = _job.brust_time;
		remaining_time_ = _job.brust_time;
		response_ratio_ = 0f;
		response_time_ = -1;
	}

	public virtual void init(Job _job)
	{
		process_no_ = _job.job_no;
		arrival_time_ = _job.arrival_time;
		burst_time_ = _job.brust_time;
		remaining_time_ = _job.brust_time;
		response_ratio_ = 0f;
		response_time_ = -1;
	}

	public virtual bool isRun() { return true; }

	public virtual void tick(int _total_tick, int _work)
	{
		if (response_time_ == -1)
		{
			setResponseTime(_total_tick);
		}
		remaining_time_ -= _work;
		if (remaining_time_ < 0)
		{
			remaining_time_ = 0;
		}
		UIManager.instance.job_table_ui.updateRemainingTime(no, remaining_time_);
	}

	public void setBurstTime(int _burst_time)
    {
		remaining_time_ = _burst_time;
    }

	public void setEndTime(int t)
    {
		end_time_ = t;
    }

	public void setResponseRatio(int _total_tick)
	{
		response_ratio_ = (float)(_total_tick - arrival_time_ + burst_time) / (float)burst_time;
	}

	public void setResponseTime(int _total_tick)
    {
		response_time_ = _total_tick;
	}

	public virtual void finishProcess()
    {
		UIManager.instance.finish_process_table_ui.updateProcess(this);
		SchedulerManager.instance.completeProcess();
	}
}