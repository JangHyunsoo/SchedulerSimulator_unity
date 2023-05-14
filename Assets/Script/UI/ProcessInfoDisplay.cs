using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessInfoDisplay : Singleton<ProcessInfoDisplay>
{
    private Process cur_process_;

    [SerializeField]
    private RectTransform rect_tranform_;
    [SerializeField]
    private Text no_text_;
    [SerializeField]
    private Text at_text_;
    [SerializeField]
    private Text bt_text_;
    [SerializeField]
    private Text rm_text_;

    private bool use_ui_ = true;
    public bool use_ui { get => use_ui_; }

    public void toggleUseUI()
    {
        use_ui_ = !use_ui_;
    }

    public void openUI(Process _process)
    {
        cur_process_ = _process;
        upateUI();
        rect_tranform_.gameObject.active = true;
    }

    public void closeUI()
    {
        rect_tranform_.gameObject.active = false;
    }

    public void upateUI()
    {
        if(cur_process_ != null)
        {
            no_text_.text = cur_process_.no.ToString();
            at_text_.text = cur_process_.arrival_time.ToString();
            bt_text_.text = cur_process_.burst_time.ToString();
            rm_text_.text = cur_process_.remaining_time.ToString();
        }
    }

    public void LateUpdate()
    {
        Vector2 mousePos = Input.mousePosition;
        rect_tranform_.position = mousePos + new Vector2(85, 70);
    }
}
