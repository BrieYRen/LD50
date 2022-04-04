using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorSwitcher : MonoBehaviour
{
    public Texture2D normal;
    Vector2 offset = new Vector2(0, 0);

    private void Start()
    {
        SetNormal();
    }

    public void SetNormal()
    {
        Cursor.SetCursor(normal, offset, CursorMode.Auto);
    }

    public void SetToCursor(Texture2D newCursor, Vector2 newOffset)
    {
        Cursor.SetCursor(newCursor, newOffset, CursorMode.Auto);
    }
}
