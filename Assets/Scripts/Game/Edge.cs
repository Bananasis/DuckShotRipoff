using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D c)
    {
        Game.instance.perfect = false;
    }
}
