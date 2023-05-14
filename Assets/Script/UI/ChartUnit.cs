using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChartUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Process process_;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (process_ != null && ProcessInfoDisplay.instance.use_ui)
        {
            ProcessInfoDisplay.instance.openUI(process_);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ProcessInfoDisplay.instance.closeUI();
    }

    public void setColor(Color _color)
    {
        GetComponent<Image>().color = _color;
    }

    public void setProcess(Process _process)
    {
        process_ = _process;
    }
}
