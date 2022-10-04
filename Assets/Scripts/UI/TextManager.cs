using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TextPool bounces;
    [SerializeField] private TextPool plusOnes;
    [SerializeField] private TextPool perfects;

    [SerializeField] private float speed;
    [SerializeField] private float fading;
    public void SpawnBounce(Vector3 position)
        => bounces.Spawn(position+Vector3.up/10, speed*Vector3.up, fading);
    public void SpawnplusOne(Vector3 position,int points)
        => plusOnes.Spawn(position, speed*Vector3.up, fading, $"+{points}");
    public void Spawnperfect(Vector3 position)
        => perfects.Spawn(position-Vector3.up/10, speed*Vector3.up, fading);
}
