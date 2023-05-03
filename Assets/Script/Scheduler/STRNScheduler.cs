using System;
using System.Collections.Generic;

public class STRNScheduler : Scheduler
{
    public class STRNCompare : IComparer<Process>
    {
        public int Compare(Process one, Process other)
        {
            if (one.cur_burst_time == other.cur_burst_time)
            {
                return 0;
            }
            else return other.cur_burst_time - one.cur_burst_time;
        }
    }

    private PriorityQueue<Process> process_queue_ = new PriorityQueue<Process>(new STRNCompare());

    public override void init()
    {
        process_queue_.clear();
    }

    public override void logic(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;

        Queue<Process> arrival_process_queue = getArrivalProcess(_total_tick);

        while (arrival_process_queue.Count != 0)
        {
            process_queue_.push(arrival_process_queue.Dequeue());
        }

        while (process_queue_.count != 0)
        {
            var max_pair = psr_mgr.maxCurBurstTime();
            var process = process_queue_.peek();

            if(max_pair.Value <= process.cur_burst_time)
            {
                break;
            }
            else
            {
                Processor processor = psr_mgr.getProcessor(max_pair.Key);
                Process swap_process = processor.swapProcess(process);
                process_queue_.pop();
                if(swap_process != null)
                {
                    process_queue_.push(swap_process);
                }
            }
        }

        psr_mgr.tick(_total_tick);
    }
}
