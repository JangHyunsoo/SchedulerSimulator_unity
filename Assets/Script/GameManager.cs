using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        init();
        run();
    }

    private void init()
    {
        initManager();
    }

    private void initManager()
    {
        JobSimulator.instance.init();
        ProcessorManager.instance.init();
        SchedulerManager.instance.init();
        UIManager.instance.init();

        
    }

    public void run()
    {
        SchedulerManager.instance.run();
    }
}
