using System.Collections;
using System.Collections.Generic;

class RRScheduler : Scheduler
{
    private Queue<Process> process_queue_ = new Queue<Process>();
    private int time_quantum_;

    public RRScheduler(int _time_quantum)
    {
        time_quantum_ = _time_quantum;
    }

    public override void init()
    {
        process_queue_.Clear();
    }

    public override void queuing(int _total_tick)
    {
        Queue<RRProcess> arrival_process_queue = getArrivalProcess<RRProcess>(_total_tick);

        while (arrival_process_queue.Count != 0)
        {
            process_queue_.Enqueue(arrival_process_queue.Dequeue());
        }

        UIManager.instance.chart_process_queue_ui.updateUI(process_queue_.ToArray());
    }

    public override void logic(int _total_tick)
    {
        var psr_mgr = ProcessorManager.instance;

        while (psr_mgr.countAvailable() > 0 && process_queue_.Count != 0)
        {
            Processor processor = psr_mgr.getAvailableProcessor();
            Process process = process_queue_.Dequeue();
            Process swap_process = processor.swapProcess(process);
            ((RRProcess)process).setQuantumTime(time_quantum_);
            if(swap_process != null)
            {
                process_queue_.Enqueue(swap_process);
            }
        }

        psr_mgr.tick(_total_tick);
    }
}