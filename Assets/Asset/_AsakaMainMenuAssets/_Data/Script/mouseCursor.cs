using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mouseCursor : MonoBehaviour
{
    public GameObject cursor;
    public Image spriteRenderer;
    public Sprite normalCursor;
    public Sprite pointerCursor;

    public GameObject clickEffect;

    private void Start()
    {
        Cursor.visible = false;
        //spriteRenderer = GetComponent<Image>();
    }

    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cursorPos1 =
        cursor.transform.position = cursorPos;
        transform.position = cursorPos;

    if (Input.GetMouseButton(0))
        {
            spriteRenderer.sprite = normalCursor;
        }
    }
}
