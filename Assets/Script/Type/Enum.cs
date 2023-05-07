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
	OUR
}

public struct CoreCount
{
	public int e_count { get; set; }
	public int p_count { get; set; }
}