using UnityEngine;
using UnityEngine.Assertions;

namespace SiberUtility.Tools
{
    /// <summary> 常見的工具 </summary>
    public static class CommonHelper
    {
        public static bool ToggleActive(this GameObject gameObject)
        {
            var toggleActive = !gameObject.activeSelf;
            gameObject.SetActive(toggleActive);
            return toggleActive;
        }

        /// <summary> bool 切換 </summary>
        public static void Toggle(ref bool boolean)
        {
            boolean = !boolean;
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

        public static void SetFlipXY(this SpriteRenderer spriteRenderer, bool xisActive, bool yisActive)
        {
            if (spriteRenderer == null) return;
            spriteRenderer.flipX = xisActive;
            spriteRenderer.flipY = yisActive;
        }

        public static void SetFlipX(this SpriteRenderer spriteRenderer, bool isActive)
        {
            if (spriteRenderer == null) return;
            spriteRenderer.flipX = isActive;
        }

        public static void SetFlipY(this SpriteRenderer spriteRenderer, bool isActive)
        {
            if (spriteRenderer == null) return;
            spriteRenderer.flipY = isActive;
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

    public static class EnableHelper
    {
        /// <summary> 擴展同: gameObject.SetActive(bool) </summary>
        public static void SetActive(this Component component, bool isActive)
        {
            if (component == null) return;
            component.gameObject.SetActive(isActive);
        }

        /// <summary> 擴展同: enabled = bool </summary>
        public static void SetEnable(this Renderer renderer, bool isEnable)
        {
            if (renderer == null) return;
            renderer.enabled = isEnable;
        }

        /// <summary> 擴展同: enabled = bool </summary>
        public static void SetEnable(this Behaviour behaviour, bool isEnable)
        {
            if (behaviour == null) return;
            behaviour.enabled = isEnable;
        }
    }
}