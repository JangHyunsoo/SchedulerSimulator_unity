using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioSlot : MonoBehaviour
{
    private int no_;
    public int no { get =>  no_;}
    [SerializeField]
    private Text text_;

    public void setText(string _text)
    {
        text_.text = _text;
    }

    public void setNo(int _no)
    {
        no_ = _no;
    }

    public void onClickButton()
    {
        SetUpManager.instance.scenario_table_ui.selectButton(no_);
    }
}
