using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Game.instance.bounce = true;
        SoundManager.instance.Play("Impact");
    }
}
