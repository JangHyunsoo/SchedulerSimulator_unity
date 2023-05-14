using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour
{
    public void onClickLoadBtn(string _scene_name)
    {
        SceneManager.LoadScene(_scene_name);
    }
}
