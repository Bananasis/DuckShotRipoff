
    using System;
    using UnityEngine;

    public static class Utils
    {
        public static float SharpStep(float a, float b, float d)
        {
            var c = a - b;
            if (Mathf.Abs(c) < d)
                return b;
            if (c > 0) return a - d;
            return a + d;
        }
        
        public static Vector3 SharpStep(Vector3 a, Vector3 b, Vector3 d)
        {

            return new Vector3(SharpStep(a.x, b.x, d.x), SharpStep(a.y, b.y, d.y), SharpStep(a.z, b.z, d.z));
        }
        

        
        
        public static (bool,float) Bounce(float a, float l, float r)
        {
            if (a > r) return (true,2 * r - a);
            return a < l ? (true,2 * l - a) : (false, a);
        }
         public class GameException: Exception
         {
             public GameException(string message) : base(message)
             {
             }
         }
    }
