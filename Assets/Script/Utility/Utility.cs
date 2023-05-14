using System;
using System.Collections.Generic;
using System.Linq;

public class Utility
{
    public static Dictionary<ScheduleWay, string> schedule_dic_ = new Dictionary<ScheduleWay, string>()
    {
        { ScheduleWay.FCFS, "FCFS"},
        { ScheduleWay.RR, "RR"},
        { ScheduleWay.SPN, "SPN"},
        { ScheduleWay.SRTN, "SRTN"},
        { ScheduleWay.HRRN, "HRRN"},
        { ScheduleWay.DPS, "DPS"},
    };

    public static double standardDeviation(IEnumerable<double> sequence)
    {
        double result = 0;

        if (sequence.Any())
        {
            double average = sequence.Average();
            double sum = sequence.Sum(d => Math.Pow(d - average, 2));
            result = Math.Sqrt((sum) / sequence.Count());
        }

        return result;
    }

    public static double average(List<double> sequence)
    {
        double result = 0;

        foreach (double i in sequence)
        {
            result += i;
        }

        result /= sequence.Count();

        return result;
    }
}