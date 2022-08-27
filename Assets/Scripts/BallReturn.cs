using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BallReturn : MonoBehaviour
{
    private BallLauncher ballLauncher;
    private void Awake()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ballLauncher.ReturnBall();
        collision.collider.gameObject.SetActive(false);
    }
}
