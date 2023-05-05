using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartProcessorUnit : MonoBehaviour
{
    [SerializeField]
    private Image processor_image_;
    [SerializeField]
    private Image background_image_;

    public void setColor(Color color)
    {
        background_image_.color = color;
    }
}
