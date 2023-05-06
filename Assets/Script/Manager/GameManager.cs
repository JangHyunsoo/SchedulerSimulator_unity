using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        init();
        // run();
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
    
    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
