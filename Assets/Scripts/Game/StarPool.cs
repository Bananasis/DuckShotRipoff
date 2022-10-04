using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPool : MonoBehaviour
{
    private Queue<Star> pool = new Queue<Star>();
    [SerializeField] private Transform starPoint;
    [SerializeField] private Star starPref;
   

    

    public void Spawn(Vector3 position)
    {
        if (pool.Count == 0)
        {
            pool.Enqueue(Instantiate(starPref,position,Quaternion.identity,transform));
        }
        var star = pool.Dequeue();
        star.transform.position = position;
        StartCoroutine(star.ReturnToPool(pool,starPoint));
    }
    
}
