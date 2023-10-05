using UnityEngine;

namespace SiberUtility.Tools
{
    /// <summary> Vector工具
    /// <para> (Distance , IsCloseSomthing?)</para>
    /// </summary>
    public static class VectorHelper
    {
    #region ========== [Public Methods] ==========

        /// <summary> 自製檢測 Vector2 距離 </summary>
        /// <param name="pointA"> A點(this) </param>
        /// <param name="pointB"> B點 </param>
        /// <returns></returns>
        public static float Distance(this Vector2 pointA, Vector2 pointB)
        {
            var distance = (pointA - pointB).sqrMagnitude;
            return distance;
        }

        /// <summary> 判斷 *兩點距離* 是否接近指定向量長度 </summary>
        /// <param name="pointA"> A點 </param>
        /// <param name="pointB"> B點 </param>
        /// <param name="magnitude"> 依據的向量長度 </param>
        /// <returns></returns>
        public static bool IsCloseThePoint(Vector2 pointA, Vector2 pointB, float magnitude)
        {
            var distance = Distance(pointA, pointB);
            return distance <= magnitude;
        }

    #endregion
    }
}