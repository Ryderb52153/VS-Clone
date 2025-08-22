using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;  
    [SerializeField] private Vector2 clickPoint = Vector2.zero; 
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, clickPoint, cursorMode);
    }

}
