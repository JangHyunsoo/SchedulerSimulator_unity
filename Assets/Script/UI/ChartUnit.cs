using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartUnit : MonoBehaviour
{
    public void setColor(Color _color)
    {
        GetComponent<Image>().color = _color;
    }
}
