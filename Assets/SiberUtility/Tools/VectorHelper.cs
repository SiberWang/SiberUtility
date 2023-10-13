using UnityEngine;
using UnityEngine.Assertions;

namespace SiberUtility.Tools
{
    /// <summary> Vector工具
    /// <para> (Distance , IsCloseSomthing?)</para>
    /// </summary>
    public static class VectorHelper
    {
    #region ========== [Public Methods] ==========

        /// <summary> AB 距離 (sqrMagnitude) </summary>
        /// <param name="pointA"> A點(this) </param>
        /// <param name="pointB"> B點 </param>
        public static float Distance_By_SqrMagnitude(this Vector2 pointA, Vector2 pointB)
        {
            var distance = (pointA - pointB).sqrMagnitude;
            return distance;
        }

        /// <summary> AB 距離 (Magnitude) </summary>
        /// <param name="pointA"> A點(this) </param>
        /// <param name="pointB"> B點 </param>
        public static float Distance_By_Magnitude(this Vector2 pointA, Vector2 pointB)
        {
            var distance = (pointA - pointB).magnitude;
            return distance;
        }

        /// <summary> AB 向量 </summary>
        /// <param name="pointA"> A點(this) </param>
        /// <param name="pointB"> B點 </param>
        public static Vector2 Direction(this Vector2 pointA, Vector2 pointB)
        {
            var distance = (pointA - pointB).normalized;
            return distance;
        }

        /// <summary> 是否在距離內 (SqrMagnitude) </summary>
        /// <param name="pointA"> A點 </param>
        /// <param name="pointB"> B點 </param>
        /// <param name="magnitude"> 依據的向量長度 </param>
        public static bool IsCloseThePoint(Vector2 pointA, Vector2 pointB, float magnitude)
        {
            var distance    = Distance_By_SqrMagnitude(pointA, pointB);
            var doubleValue = magnitude * magnitude;
            return distance <= doubleValue;
        }

        /// <summary> 貝賽爾曲線 </summary>
        /// https://youtu.be/c3H5qi0WHHw?si=TKAt2jvSc6Ku3FPg 參考來源
        public static Vector2 Bezier(float time, Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            var ab = Vector2.Lerp(pointA, pointB, time);
            var bc = Vector2.Lerp(pointB, pointC, time);
            return Vector2.Lerp(ab, bc, time);
        }

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

    #endregion
    }
}