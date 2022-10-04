using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prediction : MonoBehaviour
{
    [SerializeField] private PredictiveDot dotPrefab;
    [SerializeField] private List<PredictiveDot> predictiveDots;
    [SerializeField] private int dotNumber ;

    [SerializeField] private float timeStep;
    private bool hidden = false;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < dotNumber; i++)
        {
            var dot = Instantiate(dotPrefab, transform);
            predictiveDots.Add(dot);
        }
    }

    private Vector2 lineStart;
    private Vector2 ballVelocity;
  
    private float baseTransparency;
   
    public void Update()
    {
        if (hidden)
            return;
        var time = (Time.unscaledTime%1f)*timeStep;
        var transparency = baseTransparency/2;
        var dTransparency = 0.5f / dotNumber;
        var velocity = ballVelocity;
        var dotPosition = lineStart+time*ballVelocity.x*Vector2.right;
        
        for (int i = 0; i < dotNumber; i++)
        {
            
            dotPosition = new Vector2(dotPosition.x,lineStart.y + ballVelocity.y * time - time * time / 2 * 9.81f);
            bool bounced;
            (bounced, dotPosition.x) = Utils.Bounce(dotPosition.x, Game.instance.gm.leftBounceBorder, Game.instance.gm.rightBounceBorder);
            velocity.x = bounced ? -velocity.x : velocity.x;
            predictiveDots[i].UpdateDot(transparency, dotPosition);
            
            dotPosition.x += velocity.x * timeStep;
            transparency -= dTransparency;
            time += timeStep;
        }
    }

    public void UpdateLine(Vector2 position, Vector2 velocity, float transparency)
    {
        hidden = false;
        lineStart = position;
        ballVelocity = velocity;
        baseTransparency = transparency;
    }

    public void HideLine()
    {
        if (hidden) return;
        hidden = true;
        foreach (var predictiveDot in predictiveDots)
        {
            predictiveDot.Hide();
        }
    }
}