using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class ResultBuilder : Singleton<ResultBuilder>
{
    private ResultInfo cur_result_info_ = new ResultInfo();
    private List<Process> complete_process_list_ = new List<Process>();

    public void addCompleteProcess(Process process)
    {
        complete_process_list_.Add(process);
    }

    public void calResultInfo()
    {
        cur_result_info_.schedule_way = SceneDataManager.instance.schedule_way;
        cur_result_info_.core_count = ProcessorManager.instance.processor_count;
        cur_result_info_.p_core_count = ProcessorManager.instance.p_core_count;
        cur_result_info_.e_core_count = ProcessorManager.instance.e_core_count;
        cur_result_info_.process_count = JobSimulator.instance.job_size;

        List<double> at_list = new List<double>();
        List<double> bt_list = new List<double>();
        List<double> wt_list = new List<double>();
        List<double> tt_list = new List<double>(); 
        List<double> ntt_list = new List<double>();

        foreach (var process in complete_process_list_)
        {
            at_list.Add(process.arrival_time);
            bt_list.Add(process.burst_time);
            wt_list.Add(process.waiting_time);
            tt_list.Add(process.turn_around_time);
            ntt_list.Add(process.normalized_turn_around_time);
        }

        cur_result_info_.at_mean = (float)Utility.average(at_list);
        cur_result_info_.at_stdev = (float)Utility.standardDeviation(at_list);
        cur_result_info_.bt_mean = (float)Utility.average(bt_list);
        cur_result_info_.bt_stdev = (float)Utility.standardDeviation(bt_list);
        cur_result_info_.waiting_time_mean   = (float)Utility.average(wt_list);
        cur_result_info_.turn_around_time_mean = (float)Utility.average(tt_list);
        cur_result_info_.normalized_turn_around_time_mean = (float)Utility.average(ntt_list);
    }

    public ResultInfo getCurResultInfo()
    {
        return cur_result_info_;
    }
}
