using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpManager : Singleton<SetUpManager>
{
    private int p_core_count_;
    public int p_core_count { get => p_core_count_; set => p_core_count_ = value; }

    private int e_core_count_;
    public int e_core_count { get => e_core_count_; set => e_core_count_ = value; }

    private int time_quantum_;
    public int time_quantum { get => time_quantum_; set => time_quantum_ = value; }

    private ScheduleWay schedule_way_;
    public ScheduleWay schedule_way { get => schedule_way_; set => schedule_way_ = value; }

    private List<Job> job_list_ = new List<Job>();
    public List<Job> job_list { get => job_list_; }

    [SerializeField]
    private ProcessorSetUpUI processor_set_up_ui_;

    public void Start()
    {
        var sdm = SceneDataManager.instance;
        p_core_count_ = sdm.p_core_count;
        e_core_count_ = sdm.e_core_count;
        schedule_way_ = sdm.schedule_way;
        time_quantum_ = sdm.time_quantum;

        var job_arr = sdm.getJobs();

        foreach (var job in job_arr)
        {
            job_list_.Add(job);
        }

        processor_set_up_ui_.init();
    }

    public void addJob(int _at, int _bt)
    {
        job_list_.Add(new Job { job_no = job_list_.Count, arrival_time = _at, brust_time = _bt });
    }

    public void increasePCore()
    {
        p_core_count_++;
    }

    public void reducePCore()
    {
        if (p_core_count_ > 0)
        {
            p_core_count_--;
            processor_set_up_ui_.deletePcore();
        }
    }

    public void increaseECore()
    {
        e_core_count_++;
    }

    public void reduceECore()
    {
        if (e_core_count_ > 0)
        {
            e_core_count_--;
            processor_set_up_ui_.deleteEcore();
        }
    }
}
