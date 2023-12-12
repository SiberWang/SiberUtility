using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

        /// <summary> 比 Unity 的 Vector2.Distance 更快！ </summary>
        public static float Distance(Vector2 a, Vector2 b)
        {
            float x = a.x - b.x;
            float y = a.y - b.y;
            return Mathf.Sqrt(x * x + y * y);
        }

    #region ========== [Lazy Direction] ==========

        public static DirectionType GetRandomDirectionType()
        {
            var directionTypes = Enum.GetValues(typeof(DirectionType));
            var randomIndex    = Random.Range(0, directionTypes.Length);
            var directionType  = (DirectionType)directionTypes.GetValue(randomIndex);
            return directionType;
        }
        
        public static Vector2 GetRandomDirection()
        {
            var directionType = GetRandomDirectionType();
            return GetDirection(directionType);
        }

        /// <summary> 獲得 Vector2 方向 (ex: 上、下、左、右) </summary>
        /// <param name="directionType"> 選:上、下、左、右、上左、上右、下左、下右 </param>
        public static Vector2 GetDirection(DirectionType directionType)
        {
            return directionType switch
            {
                DirectionType.上  => Vector2.up,
                DirectionType.下  => Vector2.down,
                DirectionType.左  => Vector2.left,
                DirectionType.右  => Vector2.right,
                DirectionType.上左 => Vector2.up + Vector2.left,
                DirectionType.上右 => Vector2.up + Vector2.right,
                DirectionType.下左 => Vector2.down + Vector2.left,
                DirectionType.下右 => Vector2.down + Vector2.right,
                _                => Vector2.zero
            };
        }

        /// <summary> 獲得 Vector2 反相的方向 (ex: 上 = 下 , 左上 = 右下) </summary>
        /// <param name="directionType"> 選:上、下、左、右、上左、上右、下左、下右 </param>
        public static Vector2 GetReverseDirection(DirectionType directionType)
        {
            return directionType switch
            {
                DirectionType.上  => Vector2.down,
                DirectionType.下  => Vector2.up,
                DirectionType.左  => Vector2.right,
                DirectionType.右  => Vector2.left,
                DirectionType.上左 => Vector2.down + Vector2.right,
                DirectionType.上右 => Vector2.down + Vector2.left,
                DirectionType.下左 => Vector2.up + Vector2.right,
                DirectionType.下右 => Vector2.up + Vector2.left,
                _                => Vector2.zero
            };
        }

        public enum DirectionType
        {
            上,
            下,
            左,
            右,
            上左,
            上右,
            下左,
            下右,
        }

    #endregion

    #endregion
    }
}