using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    private List<Ball> balls = new List<Ball>();
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    private int ballsReady;
    private LaunchPreview launchPreview;
    private BlockSpawner blockSpawner;
    
    private void Awake()
    {
        blockSpawner = FindObjectOfType<BlockSpawner>(); 
        launchPreview = GetComponent<LaunchPreview>();
        CreateBall();
    }

    private void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;
        
        if (Input.GetMouseButtonDown(0))
            StartDrag(worldPosition);
        else if (Input.GetMouseButton(0))
            ContinueDrag(worldPosition);
        else if (Input.GetMouseButtonUp(0))
            EndDrag();
    }

    public void ReturnBall()
    {
        ballsReady += 1;
        if (ballsReady == balls.Count)
        {
            CreateBall();
            blockSpawner.SpawnRowOfBlocks();
        }
    }

    private void CreateBall()
    {
        Vector3 ballTransform = new Vector3(-0.049f, -3.515f, 0f);
        var ball = Instantiate(ballPrefab, ballTransform, Quaternion.identity);
        balls.Add(ball);
        ballsReady += 1;
    } 
    
    private void EndDrag()
    {
        StartCoroutine(LaunchBalls());
    }

    private IEnumerator LaunchBalls()
    {
        Vector3 direction = (endDragPosition - startDragPosition).normalized;
        direction.Normalize();
        
        foreach (var ball in balls)
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction);
            GetComponent<LineRenderer>().enabled = false;
            
            yield return new WaitForSeconds(0.075f);
        }
        ballsReady = 0;
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        endDragPosition = worldPosition;
        Vector3 direction = endDragPosition - startDragPosition;
        launchPreview.SetEndPoint(transform.position - direction);
    }

    private void StartDrag(Vector3 worldPosition)
    {
        startDragPosition = worldPosition; 
        launchPreview.SetStartPoint(transform.position);
        GetComponent<LineRenderer>().enabled = true;
    }
}
