using UnityEngine;

namespace SiberUtility.Tools
{
    public static class ColorHelper
    {
        public static Color GetColorByHtml(string htmlString, float alpha = 1f)
        {
            var color = Color.white;
            if (ColorUtility.TryParseHtmlString("#" + htmlString, out var htmlColor))
                color = htmlColor;
            color.a = alpha;
            return color;
        }
        
        /// <summary> Color 轉為 Html , Hex </summary>
        public static string ToHtml(this Color color, bool includeAlpha = true)
        {
            Color32 color32   = (Color32)color;
            string  alphaPart = includeAlpha ? $"{color32.a:X2}" : "";
            return $"#{color32.r:X2}{color32.g:X2}{color32.b:X2}{alphaPart}";
        }
        
        public static Color SetAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}