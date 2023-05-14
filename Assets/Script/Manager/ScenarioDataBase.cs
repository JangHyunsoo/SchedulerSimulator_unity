using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioDataBase : Singleton<ScenarioDataBase>
{
    [SerializeField]
    private Scenario[] scenario_arr_;
    public Scenario[] scenario_arr { get { return scenario_arr_; } }

    public void Start()
    {
        foreach (var scenario in scenario_arr_)
        {
            if(scenario.jobs.Length != scenario.colors.Length)
            {
                makeColor(scenario);
            }
        }
    }

    private void makeColor(Scenario scenario)
    {
        Color[] colors = new Color[scenario.jobs.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = new Color(Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f), Random.RandomRange(0f, 1f));
        }
        scenario.colors = colors;
    }
}
