using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] private float lowerBound;
    [SerializeField] private float upperBound;
    [SerializeField] private float hardLowerBound;

    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody2D leftWall;

    [SerializeField] private Rigidbody2D rightWall;

    [SerializeField] private Collider2D deathZone;

    private float maxBallY;
    private float goalY;
    private Vector3 defPosition;
    // Start is called before the first frame update
    [SerializeField] Ball ball;
    [SerializeField]  private float dY;
    private GameModel gm => Game.instance.gm;

    private float lowerBoundOffset;
    private float upperBoundOffset;
    private float hardLowerBoundOffset;
    private void Start()
    {
        defPosition = transform.position;
        goalY = transform.position.y;
        lowerBoundOffset = gm.camWorldSize.size.y *  (lowerBound-1/ 2f);
        upperBoundOffset = gm.camWorldSize.size.y *  (upperBound-1/ 2f);
        hardLowerBoundOffset = gm.camWorldSize.size.y * hardLowerBound;
        leftWall.transform.position = new Vector3(gm.camWorldSize.min.x - gm.wallThickness, leftWall.transform.position.y);
        rightWall.transform.position = new Vector3(gm.camWorldSize.max.x + gm.wallThickness, rightWall.transform.position.y);
        deathZone.transform.position = new Vector3(deathZone.transform.position.x, gm.camWorldSize.min.y-gm.wallThickness);
    }

    public void SetToStart()
    {
        maxBallY = 0;
        transform.position = defPosition;
        goalY = defPosition.y;
    }

    private void FixedUpdate()
    {
        
        var position = transform.position;
        position = new Vector3(position.x, Mathf.Lerp(position.y, goalY, 0.01f));
        transform.position = position;
        var ballY = ball.transform.position.y;
        maxBallY = maxBallY > ballY ? maxBallY : ballY;
        if (transform.position.y + upperBoundOffset < ballY)

        {
            goalY += dY*Time.deltaTime;
            return;
            
        }

        if (maxBallY + hardLowerBoundOffset > ballY)
        {
            return;
        }
        
        if (transform.position.y + lowerBoundOffset > ballY)
        {
            goalY -= dY*Time.deltaTime;
            return;
        }

        goalY = Mathf.Lerp(goalY, position.y, 0.1f);
    }
}
