using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Net : MonoBehaviour
{
    public Transform slingPoint;
    [SerializeField] private Transform slingPointD;
    [SerializeField] private Transform netTransform;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    [SerializeField] private float offsetC;
    [SerializeField] private float offsetBallC;
    [SerializeField] private float stretchingSpeed;

    private float defaultOffset;
    private float currentStretch;
    private float goalStretch;
    

    public void Pull(float stretch, Quaternion rotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
        goalStretch = stretch;
    }

    public void Unstretch()
    {
        goalStretch = 0;
    }

    void Start()
    {
        netTransform.localScale = new Vector3(netTransform.localScale.x,minScale,1);
        defaultOffset = netTransform.localPosition.y;
    }

    private void Stretch()
    {
        var stretchLerp = Mathf.Lerp(minScale, maxScale, currentStretch);

        netTransform.localScale = new Vector3(netTransform.localScale.x, stretchLerp);
        var position = netTransform.localPosition;
        position = new Vector3(position.x, (minScale - stretchLerp) * offsetC + defaultOffset);
        netTransform.localPosition = position;
        var transformS = slingPoint.transform;
        transformS.localPosition = new Vector3(transformS.localPosition.x,
            -currentStretch * offsetBallC + slingPointD.transform.localPosition.y);
    }

    public void Update()
    {
        if (goalStretch == currentStretch) return;
        currentStretch = Utils.SharpStep(currentStretch, goalStretch, stretchingSpeed);
        Stretch();
    }
}