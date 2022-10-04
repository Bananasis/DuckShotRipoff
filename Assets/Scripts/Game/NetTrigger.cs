using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetTrigger : MonoBehaviour
{        
    [SerializeField] private Net net;
    [SerializeField] private Collider2D ballCollider;
    
    
    
    private void OnTriggerStay2D(Collider2D ballC)
    {
        if (ballCollider != ballC)
            return;
        Game.instance.Magnetize(net);
    }

    

}
