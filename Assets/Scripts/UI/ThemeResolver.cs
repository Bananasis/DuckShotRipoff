using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeResolver : MonoBehaviour
{
    [SerializeField]
    private List<Image> images = new List<Image>();
    [SerializeField]
    private Material night;
    [SerializeField]
    private Material day;

    public void Swith(bool isNight)
    {
        
        foreach (var image in images)
        {
            image.material = isNight ? night : day;
        }
    }
}
