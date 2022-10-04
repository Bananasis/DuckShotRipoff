using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPool : MonoBehaviour
{
    private Queue<TextMeshProUGUI> pool = new Queue<TextMeshProUGUI>();

    [SerializeField] private TextMeshProUGUI label;
   

    

    public void Spawn(Vector3 position,Vector3 speed,float fading,string text = "")
    {
        if (pool.Count == 0)
        {
            pool.Enqueue(Instantiate(label,position,Quaternion.identity,transform));
        }
        var tmp = pool.Dequeue();
        if (text != "") tmp.text = text;
        StartCoroutine(Display(position,speed,fading,tmp));

    }
    

    IEnumerator Display(Vector3 position,Vector3 speed,float fading,TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
        text.transform.position = position;
        var color = text.color;
        color.a = 1f;
        text.color = color;
        while (color.a > 0)
        {
            color = text.color;
            color.a = Utils.SharpStep(color.a, 0, fading);
            text.color = color;
            text.transform.position += speed;
            yield return null;
        }
        text.gameObject.SetActive(false);
        pool.Enqueue(text);
    }
}
