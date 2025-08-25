using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;  
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Vector2 clickPoint = Vector2.zero; 
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

    private void Start()
    {
        SetDefaultCursor();
    }


    public void SetDefaultCursor()
    {
        Cursor.SetCursor (cursorDefault, clickPoint, cursorMode);
    }

    public void SetTargetCursor()
    {
        Cursor.SetCursor(cursorTexture, clickPoint, cursorMode);
    }
}