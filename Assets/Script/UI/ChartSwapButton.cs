using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ChartSwapButton : MonoBehaviour
{
    private bool can_swap_ = false;
    public bool can_swap { get => can_swap_; set => can_swap_ = value; }

    [SerializeField]
    private Transform up_panel_;
    [SerializeField]
    private Transform down_panel_;

    [SerializeField]
    private Transform chart_panel_;
    [SerializeField]
    private Transform table_panel_;

    public void onClickChart()
    {

        chart_panel_.SetParent(up_panel_);
        table_panel_.SetParent(down_panel_);
    }

    public void onClickTable()
    {
        if (can_swap_)
        {
            chart_panel_.SetParent(down_panel_);
            table_panel_.SetParent(up_panel_);
        }
        else
        {
            Debug.Log(can_swap);
        }
    }
}
