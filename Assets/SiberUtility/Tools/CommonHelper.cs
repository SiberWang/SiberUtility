using UnityEngine;

namespace SiberUtility.Tools
{
    /// <summary> 常見的工具 </summary>
    public static class CommonHelper
    {
        /// <summary> bool 切換 </summary>
        public static void Toggle(ref bool boolean)
        {
            boolean = !boolean;
        }
    }

    public static class TrailRendererHelper
    {
        /// <summary> TrailRenderer 設置啟用 </summary>
        public static void SetEnable(this TrailRenderer trailRenderer, bool isEnable)
        {
            if (trailRenderer == null) return;
            trailRenderer.enabled = isEnable;
        }

        public static void InitTrail(this TrailRenderer trailRenderer)
        {
            if (trailRenderer == null) return;
            trailRenderer.Clear();
            trailRenderer.time = 0f;
        }

        public static void SetTime(this TrailRenderer trailRenderer, float time)
        {
            if (trailRenderer == null) return;
            trailRenderer.time = time;
        }
    }

    public static class SpriteRendererHelper
    {
        /// <summary> SpriteRenderer 設置啟用 </summary>
        public static void SetEnable(this SpriteRenderer spriteRenderer, bool isEnable)
        {
            if (spriteRenderer == null) return;
            spriteRenderer.enabled = isEnable;
        }
    }
}