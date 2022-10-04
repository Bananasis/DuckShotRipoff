using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Star : MonoBehaviour
{
    private bool taken;

    [SerializeField] float speed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (taken) return;
        SoundManager.instance.Play("Star");
        taken = true;
    }

    public IEnumerator ReturnToPool(Queue<Star> pool, Transform starPoint)
    {
        taken = false;
        gameObject.SetActive(true);
        while (!taken)
        {
            yield return null;
        }

        while ( (starPoint.position - transform.position).sqrMagnitude >0.03)
        {
            transform.position = Utils.SharpStep(transform.position, starPoint.position,
                (starPoint.position - transform.position).normalized * speed * Time.deltaTime);
            yield return null;
        }

        pool.Enqueue(this);
        gameObject.SetActive(false);
        Game.instance.AddStar();
    }
}