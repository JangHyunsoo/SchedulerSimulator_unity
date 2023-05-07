using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchedulerManager : Singleton<SchedulerManager>
{
    private int total_tick_;
	private int complete_process_ = 0;
    private Scheduler cur_scheduler_;

	[SerializeField]
	private ScheduleWay schedule_way_ = ScheduleWay.FCFS;
	[SerializeField]
	private int time_quantum = 2;

	public int total_tick { get => total_tick_; }

    public void init()
    {
        total_tick_ = 0;
        assignScheduler(schedule_way_);
		UIManager.instance.chart_process_queue_ui.init();
		queuing();
	}

	public void step()
    {
		if (!isDone())
        {
			cur_scheduler_.tick(total_tick_);
			ProcessorManager.instance.appendGanttChart(total_tick_++);
			cur_scheduler_.queuing(total_tick);
		}
	}

	public void jump()
	{
		while (!isDone())
		{
			cur_scheduler_.tick(total_tick_);
			ProcessorManager.instance.appendGanttChart(total_tick_++);
			cur_scheduler_.queuing(total_tick);
		}
	}

	public void queuing()
    {
		cur_scheduler_.queuing(total_tick);
	}

	private bool isDone()
    {
		return JobSimulator.instance.job_size == complete_process_;
	}

	private void assignScheduler(ScheduleWay _way)
    {
		switch (_way)
		{
			case ScheduleWay.FCFS:
				cur_scheduler_ = new FCFSScheduler();
				break;
			case ScheduleWay.RR:
				cur_scheduler_ = new RRScheduler(time_quantum);
				break;
			case ScheduleWay.SPN:
				cur_scheduler_ = new SPNScheduler();
				break;
			case ScheduleWay.SRTN:
				cur_scheduler_ = new STRNScheduler();
				break;
			case ScheduleWay.HRRN:
				cur_scheduler_ = new HRRNScheduler();
				break;
			case ScheduleWay.OUR:
				cur_scheduler_ = new MultiThreadScheduler();
				break;
			default:
				break;
		}
	}

	public void completeProcess()
    {
		complete_process_++;
	}
}