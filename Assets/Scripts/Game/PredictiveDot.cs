using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictiveDot : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void UpdateDot(float transparency, Vector2 position)
    {
        var color = sprite.color;
        color.a = transparency;
        sprite.color = color;
        transform.position = position;
    }
    public void Hide()
    {
        var color = sprite.color;
        color.a = 0;
        sprite.color = color;
        
    }
}
