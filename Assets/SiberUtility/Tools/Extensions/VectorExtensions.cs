using UnityEngine;

namespace SiberUtility.Tools.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 SetZero(this Vector2 point)
        {
            point = Vector2.zero;
            return point;
        }

        public static Vector3 SetZero(this Vector3 point)
        {
            point = Vector3.zero;
            return point;
        }

        public static Vector2 With(this Vector2 vector, float? x = null, float? y = null)
        {
            return new Vector2(x ?? vector.x, y ?? vector.y);
        }

        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        public static Vector2 Add(this Vector2 vector, float? x = null, float? y = null)
        {
            return new Vector2(vector.x + (x ?? 0), vector.y + (y ?? 0));
        }

        public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));
        }
        
        public static bool IsValid(this Vector2 v)
        {
            return !float.IsNaN(v.x) && !float.IsNaN(v.y) &&
                   !float.IsInfinity(v.x) && !float.IsInfinity(v.y);
        }
        
        public static float PickRandomValue(this Vector2 values)
        {
            var min      = values.x;
            var max      = values.y;
            var minValue = min >= max ? max : min;
            return Mathf.Lerp(minValue, max, Random.value);
        }

        public static int PickRandomValue(this Vector2Int values)
        {
            var min      = values.x;
            var max      = values.y;
            var minValue = min >= max ? max : min;
            var maxValue = max + 1;
            var range    = Random.Range(minValue, maxValue);
            return range;
        }
    }
}