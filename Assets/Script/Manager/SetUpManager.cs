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
    private List<Color> job_color_list_ = new List<Color>();

    [SerializeField]
    private ProcessorSetUpUI processor_set_up_ui_;

    [SerializeField]
    private ProcessInfoTable process_info_table_ui_;

    [SerializeField]
    private ScheduleSelector schedule_selector_ui_;

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
            addJob(job.arrival_time, job.brust_time);
        }

        processor_set_up_ui_.init();
        process_info_table_ui_.init();
        schedule_selector_ui_.init(schedule_way_);
    }

    public void addJob(int _at, int _bt)
    {
        job_color_list_.Add(new Color(Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f)));
        Job job = new Job { job_no = job_list_.Count, arrival_time = _at, brust_time = _bt };
        job_list_.Add(job);
        process_info_table_ui_.addJob(job);
    }

    public void addJob(Color _color, int _at, int _bt)
    {
        job_color_list_.Add(_color);
        Job job = new Job { job_no = job_list_.Count, arrival_time = _at, brust_time = _bt };
        job_list_.Add(job);
        process_info_table_ui_.addJob(job);
    }

    public void deleteJob(int _no)
    {
        job_list_.RemoveAt(_no);
        job_color_list_.RemoveAt(_no);
        for (int i = _no; i < job_list_.Count; i++)
        {
            job_list_[i].job_no = i;
        }
        process_info_table_ui_.removeJob(_no);
    }

    public Color getJobColor(int _no)
    {
        return job_color_list_[_no];
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

    public void setSchedule(ScheduleWay _way)
    {
        schedule_way_ = _way;
    }

    public void onClickApply()
    {
        if (e_core_count + p_core_count > 0)
        {
            SceneDataManager.instance.setJobArr(job_list_.ToArray());
            SceneDataManager.instance.setJobColorArr(job_color_list_.ToArray());
            SceneDataManager.instance.setECore(e_core_count_);
            SceneDataManager.instance.setPCore(p_core_count_);
            SceneDataManager.instance.setScheduleWay(schedule_way_, time_quantum_);


        }
    }
}
