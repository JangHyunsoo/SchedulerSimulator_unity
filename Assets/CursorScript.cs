using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursor_idle_;
    [SerializeField]
    private Texture2D cursor_click_;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursor_click_, new Vector2(0, 0), CursorMode.Auto);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursor_idle_, new Vector2(0, 0), CursorMode.Auto);
        }
        
    }
}
