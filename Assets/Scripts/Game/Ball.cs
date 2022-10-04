using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform slingshotPoint;
    private Rigidbody2D body;
    GameModel gm => Game.instance.gm; 

    
    private bool caught;
    

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    

    private void FixedUpdate()
    {
        if (!slingshotPoint)
            return;
        if (caught)
        {
            body.transform.position = slingshotPoint.transform.position;
            return;
        }
        var delt = slingshotPoint.position - transform.position;
        if (delt.magnitude < gm.magnetCancelRangeS)
        {
            caught = true;
            body.bodyType = RigidbodyType2D.Static;
            return;
        }

        body.velocity = delt * gm.magnetStrength;
    }

    public void MakeShot(Vector2 velocity)
    {
        SoundManager.instance.Play("Throw");
        body.bodyType = RigidbodyType2D.Dynamic;
        slingshotPoint = null;
        body.velocity = velocity;
        caught = false;
    }

    public void Magnetize(Transform point)
    {
        if (body.velocity.magnitude > gm.minCatchSpeed) return;
        slingshotPoint = point;
    }

    public void Spawn(Transform point)
    {
        slingshotPoint = point;
        transform.position = slingshotPoint.transform.position;
    }
}
