public enum ProcessorType
{
    EFFIC,
    PERFOR
}

public enum ScheduleWay
{
	FCFS,
	RR,
	SPN,
	SRTN,
	HRRN,
    DPS
}

public struct CoreCount
{
	public int e_count { get; set; }
	public int p_count { get; set; }
}

public class ResultInfo
{
    public ScheduleWay schedule_way;
    public int p_core_count;
    public int e_core_count;
    public int core_count;
    public int process_count;
    public float at_mean;
    public float at_stdev;
    public float bt_mean;
    public float bt_stdev;
    public float waiting_time_mean;
    public float turn_around_time_mean;
    public float normalized_turn_around_time_mean;
}
