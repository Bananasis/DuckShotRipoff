using System;
using DefaultNamespace;
using UnityEngine;
[Serializable]
    public class GameModel
    {
        public float minSpeed;
        public float maxSpeed;
        public float speedC;
        public Bounds camWorldSize;
        public float magnetCancelRangeS ;
        public float magnetStrength  ;
        public float minCatchSpeed;
        public float ballRadius;
        public float wallThickness;
        public float basketWallDistance;
        public float slingshotNeMinDistance;
        public float basketSpawnOffset;
        public float starChance;
        public float starOffset;
        public float leftBounceBorder => camWorldSize.min.x+ballRadius;
        public float rightBounceBorder => camWorldSize.max.x-ballRadius;
        
    }
    