using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchedulerManager : Singleton<SchedulerManager>
{
    private int total_tick_;
	private int complete_process_ = 0;
	private Scheduler cur_scheduler_;
	public Scheduler cur_scheduler { get => cur_scheduler_; }

	[SerializeField]
	private ScheduleWay schedule_way_ = ScheduleWay.FCFS;
	[SerializeField]
	private int time_quantum_ = 2;
	public int total_tick { get => total_tick_; }

	private bool start_update_ = false;
	private float update_delta_time_ = 0f;

    private void Update()
    {
        if (start_update_)
        {
			update_delta_time_ += Time.deltaTime;

			if (update_delta_time_ >= 0.2f)
            {
				update_delta_time_ -= 0.2f;
				step();
			}
        }
    }

    public void init()
    {
		schedule_way_ = SceneDataManager.instance.schedule_way;
		time_quantum_ = SceneDataManager.instance.time_quantum;
		total_tick_ = 0;
		start_update_ = false;
		assignScheduler(schedule_way_);
		UIManager.instance.chart_process_queue_ui.init();
		queuing();
	}

	public void step()
    {
		if (!isDone())
        {
			cur_scheduler_.tick(total_tick_);
            ProcessInfoDisplay.instance.upateUI();
			UIManager.instance.schedule_info_ui.updateUI();
            ProcessorManager.instance.appendGanttChart(total_tick_++);
			queuing();
            if (isDone())
            {
				ResultBuilder.instance.calResultInfo();
                UIManager.instance.resut_table_ui.setCurResultSlot();
                UIManager.instance.swap_button_ui.can_swap = true;
				SceneDataManager.instance.addResult(ResultBuilder.instance.getCurResultInfo());
            }

        }
    }

	public void jump()
	{
		while (!isDone())
		{
			step();
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
				cur_scheduler_ = new RRScheduler(time_quantum_);
				break;
			case ScheduleWay.SPN:
				cur_scheduler_ = new SPNScheduler();
				break;
			case ScheduleWay.SRTN:
				cur_scheduler_ = new SRTNScheduler();
				break;
			case ScheduleWay.HRRN:
				cur_scheduler_ = new HRRNScheduler();
				break;
			case ScheduleWay.DPS:
				cur_scheduler_ = new DPSScheduler();
				break;
			default:
				break;
		}
	}

	public void completeProcess()
    {
		complete_process_++;
	}

	public void startUpdate()
    {
		start_update_ = true;
	}

	public void pauseUpdate()
    {
		start_update_ = false;
	}
}