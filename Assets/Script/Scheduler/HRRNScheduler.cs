using System;
using System.Collections.Generic;
using UnityEngine;

public class HRRNScheduler : Scheduler
{
    public class HRRNCompare : IComparer<Process>
    {
        public int Compare(Process x, Process y)
        {
            if (x.response_ratio == y.response_ratio)
            {
                if (y.burst_time == x.burst_time) return 0;
                else return y.burst_time < x.burst_time ? 1 : -1;
            }
            else return (y.response_ratio > x.response_ratio) ? 1 : -1;
        }
    }

    // 이거 우선 순위 큐 쓸 필요 없음.
    private List<Process> process_queue_ = new List<Process>();
    private HRRNCompare hrrn_compare_ = new HRRNCompare();

    public override void init()
    {
        process_queue_.Clear();
    }

    public override void queuing(int _total_tick)
    {
        Queue<GeneralProcess> arrival_process_queue = getArrivalProcess<GeneralProcess>(_total_tick);

        while (arrival_process_queue.Count != 0)
        {
            process_queue_.Add(arrival_process_queue.Dequeue());
        }

        // 프로세서에 프로세스를 할당 가능할때만 sorting
        if (ProcessorManager.instance.canUse() && process_queue_.Count != 0)
        {
            foreach (var process in process_queue_)
            {
                process.setResponseRatio(_total_tick);
            }
            process_queue_.Sort(hrrn_compare_);
        }

        UIManager.instance.chart_process_queue_ui.updateUI(process_queue_.ToArray());
    }

    public override void logic(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;

        while (psr_mgr.canUse() && process_queue_.Count != 0)
        {
            psr_mgr.addProcess(process_queue_[0]);
            process_queue_.RemoveAt(0);
        }

        psr_mgr.tick(_total_tick);
    }
}
