using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchedulerManager : Singleton<SchedulerManager>
{
    private int total_tick_;
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
	}

	public void step()
    {
		cur_scheduler_.tick(total_tick_);
		ProcessorManager.instance.appendGanttChart(total_tick_++);
	}

	public void run()
	{
		while (!isDone())
		{
			cur_scheduler_.tick(total_tick_);
			ProcessorManager.instance.appendGanttChart(total_tick_++);
		}
	}

	private bool isDone()
    {
		bool psr_mgr_done = ProcessorManager.instance.isDone();
		bool shd_mgr_done = JobSimulator.instance.is_done;
		return psr_mgr_done && shd_mgr_done;
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
			default:
				break;
		}
	}

    
}