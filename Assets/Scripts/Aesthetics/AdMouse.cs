using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdMouse : MonoBehaviour
{
    //    [SerializeField] private RectTransform rectTransform;
    //    [SerializeField] private Transform canvas;

    //    void Update()
    //    {
    //        rectTransform.anchoredPosition = Input.mousePosition / canvas.localScale.x;
    //    }

    [SerializeField] private Texture2D texture;
    [SerializeField] private Vector2 hotspot;

    private void Start()
    {
        Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
    }
}
