using UnityEngine;

namespace SiberUtility.Tools
{
    public static class CameraHelper
    {
        /// <summary> 位置是否超出相機範圍? </summary>
        /// <param name="camera"> 使用的相機 </param>
        /// <param name="pos"> 判別的位置 </param>
        /// <param name="offset"> 偏移 , 預設為0f </param>
        public static bool IsOut2DCamera(this Camera camera, Vector2 pos, float offset = 0f)
        {
            var screenPos = (Vector2)camera.WorldToViewportPoint(pos);
            var isOutCamera = screenPos.x < -offset || screenPos.x > 1 + offset ||
                              screenPos.y < -offset || screenPos.y > 1 + offset;
            return isOutCamera;
        }

        /// <summary> 位置是否在相機範圍內? </summary>
        /// <param name="camera"> 使用的相機 </param>
        /// <param name="pos"> 判別的位置 </param>
        /// <param name="offset"> 偏移 , 預設為0f </param>
        public static bool IsIn2DCamera(this Camera camera, Vector2 pos, float offset = 0f)
        {
            return !IsOut2DCamera(camera, pos, offset);
        }

        /// <summary> 使位置受限於鏡頭內 (ViewportToWorldPoint) </summary>
        /// <param name="mainCamera"> 主要相機 </param>
        /// <param name="targetPos"> 目標位置 </param>
        /// <returns> 改變後的位置 </returns>
        /// <example> 此方法是使用 ViewportToWorldPoint 的方式來偵測並改變位置 </example>
        public static Vector2 ClampPosIn2DCameraView(this Camera mainCamera, Vector2 targetPos, float offset = 0f)
        {
            Vector2 viewportMin = mainCamera.ViewportToWorldPoint(Vector2.zero); // 左下角
            Vector2 viewportMax = mainCamera.ViewportToWorldPoint(Vector2.one);  // 右上角
            targetPos.x = Mathf.Clamp(targetPos.x, viewportMin.x + offset, viewportMax.x - offset);
            targetPos.y = Mathf.Clamp(targetPos.y, viewportMin.y + offset, viewportMax.y - offset);

            return targetPos;
        }

        /// <summary> 使位置受限於鏡頭內 (Camera Area) </summary>
        /// <param name="mainCamera"> 主要相機 </param>
        /// <param name="targetPos"> 目標位置 </param>
        /// <returns> 改變後的位置 </returns>
        /// <example> 此方法是用算的，算出相機的範圍 (Height , Width) </example>
        public static Vector2 ClampPosIn2DCamera(this Camera mainCamera, Vector2 targetPos, float offset = 0f)
        {
            // 獲得相機邊界
            float cameraHeight = 2f * mainCamera.orthographicSize; // 相機高度的兩倍
            float cameraWidth  = cameraHeight * mainCamera.aspect; // 相機寬度
            float clampX       = cameraWidth / 2f + offset;
            float clampY       = cameraHeight / 2f + offset;

            // 限制位置
            targetPos.x = Mathf.Clamp(targetPos.x, -clampX, clampX);
            targetPos.y = Mathf.Clamp(targetPos.y, -clampY, clampY);

            return targetPos;
        }

        /// <summary> 使位置受限於螢幕視窗內 (以Height為準) </summary>
        /// <param name="mainCamera"> 主要相機 </param>
        /// <param name="targetPos"> 目標位置 </param>
        /// <param name="offset"> 偏移(ex: 2.5f) </param>
        /// /// <example> 此方法是用算的，算出相機的範圍 (Height) </example>
        /// <returns> 改變後的位置 </returns>
        public static Vector2 ClampPosIn2DCameraBox(this Camera mainCamera, Vector2 targetPos, float offset = 0f)
        {
            // 獲得相機邊界
            float cameraHeight = 2f * mainCamera.orthographicSize; // 相機高度的兩倍
            float cameraWidth  = cameraHeight * mainCamera.aspect; // 相機寬度
            float clampY       = cameraHeight / 2f + offset;

            // 限制位置
            targetPos.x = Mathf.Clamp(targetPos.x, -clampY, clampY);
            targetPos.y = Mathf.Clamp(targetPos.y, -clampY, clampY);

            return targetPos;
        }
    }
}