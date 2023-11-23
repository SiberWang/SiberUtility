using UnityEngine;

namespace SiberUtility.Tools
{
    public static class RaycastHelper
    {
        /// <summary> 方向檢測 </summary>
        /// <param name="center"> 中心點 (建議 World Position) </param>
        /// <param name="direction"> 方向 (ex: Vector2.Left) </param>
        /// <param name="layerMask"> Unity Layer </param>
        /// <param name="enableDebugRay"> 顯示 Debug.DrawRay </param>
        public static bool RayDirection
            (Vector2 center, Vector2 direction, LayerMask layerMask, bool enableDebugRay = true)
        {
            var dirPos   = center + direction;
            var distance = Vector2.Distance(center, dirPos);
            var isHit    = Physics2D.Raycast(center, direction, distance, layerMask).collider != null;
            if (enableDebugRay)
                Debug.DrawRay(center, direction * distance, isHit ? Color.green : Color.red);
            return isHit;
        }

        /// <summary> 方向檢測 </summary>
        /// <param name="center"> 中心點 (建議 World Position) </param>
        /// <param name="direction"> 方向 (ex: Vector2.Left) </param>
        /// <param name="enableDebugRay"> 顯示 Debug.DrawRay </param>
        public static bool RayDirection(Vector2 center, Vector2 direction, bool enableDebugRay = true)
        {
            var dirPos   = center + direction;
            var distance = Vector2.Distance(center, dirPos);
            var isHit    = Physics2D.Raycast(center, direction, distance).collider != null;
            if (enableDebugRay)
                Debug.DrawRay(center, direction * distance, isHit ? Color.green : Color.red);
            return isHit;
        }
    }
}