using UnityEngine;

public class CursorImage : MonoBehaviour
{
    public Texture2D cursorTex;
    void Awake() { Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.ForceSoftware); }
}