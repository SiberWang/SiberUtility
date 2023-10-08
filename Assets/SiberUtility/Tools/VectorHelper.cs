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
            var distance = Distance_By_SqrMagnitude(pointA, pointB);
            return distance <= magnitude * magnitude;
        }

        /// <summary> 滑鼠位置 (World) </summary>
        /// <param name="camera"> 指定Camera </param>
        public static Vector2 GetMousePosition(Camera camera = null)
        {
            if (camera == null) camera = Camera.main;
            Assert.IsNotNull(camera, "camera == null");
            var mousePos = Input.mousePosition;
            mousePos.z = -camera.transform.position.z;
            var result = camera.ScreenToWorldPoint(mousePos);
            return result;
        }

    #endregion
    }
}