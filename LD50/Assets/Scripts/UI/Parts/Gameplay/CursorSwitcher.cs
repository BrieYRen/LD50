using UnityEngine;

/// <summary>
/// manage the mouse cursor of the game
/// </summary>
public class CursorSwitcher : MonoBehaviour
{
    [Tooltip("default mouse cursor image")]
    public Texture2D normal;

    Vector2 offset = new Vector2(0, 0);


    private void Start()
    {
        SetNormal();
    }


    /// <summary>
    /// public method to set mouse cursor to default
    /// </summary>
    public void SetNormal()
    {
        Cursor.SetCursor(normal, offset, CursorMode.Auto);
    }

    /// <summary>
    /// public method to set mouse cursor for specific requirment
    /// </summary>
    /// <param name="newCursor"></param>
    /// <param name="newOffset"></param>
    public void SetToCursor(Texture2D newCursor, Vector2 newOffset)
    {
        Cursor.SetCursor(newCursor, newOffset, CursorMode.Auto);
    }
}
