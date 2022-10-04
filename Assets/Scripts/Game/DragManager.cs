using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private Vector2 startPoint;
    public void OnDrag(PointerEventData eventData)
    {
        Game.instance.Pull(startPoint,eventData.position );
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPoint = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Game.instance.Shoot(startPoint,eventData.position);
    }
}
