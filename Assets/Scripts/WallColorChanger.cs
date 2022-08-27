using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallColorChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool colorState = false;
    private Color lerpedColor = Color.white;
    private Color newColor = new Color(255, 148, 225, 255);
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        lerpedColor = Color.Lerp(Color.white, Color.gray, Mathf.PingPong(Time.time, 1f));
        spriteRenderer.color = lerpedColor;
    }
}
