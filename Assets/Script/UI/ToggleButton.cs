using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    private Image image_;
    [SerializeField]
    private Sprite active_image_;
    [SerializeField]
    private Sprite disable_image_;
    [SerializeField]
    private bool flag = true;

    private void Start()
    {
        image_ = GetComponent<Image>();
        image_.sprite = flag ? active_image_ : disable_image_;
    }

    public void toggleImage()
    {
        flag = !flag;
        image_.sprite = flag ? active_image_ : disable_image_;
    }
}
