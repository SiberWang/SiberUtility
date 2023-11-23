using SiberUtility.Tools.Extensions;
using UnityEngine;

namespace SiberUtility.Tools
{
    public static class Collider2DHelper
    {
    #region ========== [Get Points] ==========

        public static Vector2 GetTop(this BoxCollider2D collider, float offset = 0f)
        {
            var center = collider.GetCenter();
            var size   = collider.size;
            var result = (center + new Vector2(0, size.y / 2f)).Add(y: offset);
            return result;
        }

        public static Vector2 GetBottom(this BoxCollider2D collider, float offset = 0f)
        {
            var center = collider.GetCenter();
            var size   = collider.size;
            var result = (center - new Vector2(0, size.y / 2f)).Add(y: -offset);
            return result;
        }

        public static Vector2 GetLeft(this BoxCollider2D collider, float offset = 0f)
        {
            var center = collider.GetCenter();
            var size   = collider.size;
            var result = (center - new Vector2(size.x / 2f, 0)).Add(x: -offset);
            return result;
        }

        public static Vector2 GetRight(this BoxCollider2D collider, float offset = 0f)
        {
            var center = collider.GetCenter();
            var size   = collider.size;
            var result = (center + new Vector2(size.x / 2f, 0)).Add(x: offset);
            return result;
        }

        public static Vector2 GetTopLeft(this BoxCollider2D collider, float offset = 0f)
        {
            var center = collider.GetCenter();
            var top    = GetTop(collider, offset);
            var left   = GetLeft(collider, offset);
            return top + left - center;
        }

        public static Vector2 GetTopRight(this BoxCollider2D collider, float offset = 0f)
        {
            var center = collider.GetCenter();
            var top    = GetTop(collider, offset);
            var right  = GetRight(collider, offset);
            return top + right - center;
        }

        public static Vector2 GetBottomLeft(this BoxCollider2D collider, float offset = 0f)
        {
            var center = collider.GetCenter();
            var bottom = GetBottom(collider, offset);
            var left   = GetLeft(collider, offset);
            return bottom + left - center;
        }

        public static Vector2 GetBottomRight(this BoxCollider2D collider, float offset = 0f)
        {
            var center = collider.GetCenter();
            var bottom = GetBottom(collider, offset);
            var right  = GetRight(collider, offset);
            return bottom + right - center;
        }


        /// <summary> 取得四個邊的位置 </summary>
        /// <param name="collider"> 主要Col </param>
        /// <returns> Vector2 {上、下、左、右} </returns>
        public static Vector2[] GetSidePoints(BoxCollider2D collider, float offset = 0f)
        {
            Vector2[] points = new Vector2[4];
            points[0] = collider.GetTop(offset);
            points[1] = collider.GetBottom(offset);
            points[2] = collider.GetLeft(offset);
            points[3] = collider.GetRight(offset);
            return points;
        }

        /// <summary> 取得四個轉角的位置 </summary>
        /// <param name="collider"> 主要Col </param>
        /// <returns> Vector2 {上左、上右、下左、下右} </returns>
        public static Vector2[] GetCornerPoints(BoxCollider2D collider, float offset = 0f)
        {
            Vector2[] points = new Vector2[4];
            points[0] = collider.GetTopLeft(offset);
            points[1] = collider.GetTopRight(offset);
            points[2] = collider.GetBottomLeft(offset);
            points[3] = collider.GetBottomRight(offset);
            return points;
        }

    #endregion

    #region ========== [Get Length] ==========

        public static float GetHorizontalLength(this BoxCollider2D collider, float offset = 0f)
        {
            var size = (collider.size.x / 2f) + offset;
            return size;
        }

        public static float GetVerticalLength(this BoxCollider2D collider, float offset = 0f)
        {
            var size = (collider.size.y / 2f) + offset;
            return size;
        }

        /// <summary> 取得對角線的一半 </summary>
        public static float GetHalfDiagonalLength(this BoxCollider2D collider, float offset = 0f)
        {
            var size = collider.size.Add(offset, offset) / 2f;
            return DiagonalLength(size);
        }

        /// <summary> 取得對角線 </summary>
        public static float GetDiagonalLength(this BoxCollider2D collider, float offset = 0f)
        {
            var size = collider.size.Add(offset, offset);
            return DiagonalLength(size);
        }

        /// <summary> 取得四個邊的方向 </summary>
        /// <param name="collider"> 主要Col </param>
        /// <returns> Vector2 {上、下、左、右} </returns>
        public static Vector2[] GetSideDirections(BoxCollider2D collider, float offset = 0f)
        {
            var sqrt_H = GetHorizontalLength(collider, offset);
            var sqrt_V = GetVerticalLength(collider, offset);

            Vector2[] diagonalDirs = new Vector2[4];
            diagonalDirs[0] = (collider.GetTop() - collider.offset).normalized * sqrt_V;
            diagonalDirs[1] = (collider.GetBottom() - collider.offset).normalized * sqrt_V;
            diagonalDirs[2] = (collider.GetLeft() - collider.offset).normalized * sqrt_H;
            diagonalDirs[3] = (collider.GetRight() - collider.offset).normalized * sqrt_H;
            return diagonalDirs;
        }

        /// <summary> 取得四個轉角的方向 </summary>
        /// <param name="collider"> 主要Col </param>
        /// <returns> Vector2 {上左、上右、下左、下右} </returns>
        public static Vector2[] GetCornerDirections(BoxCollider2D collider, float offset = 0f)
        {
            var sqrt = GetHalfDiagonalLength(collider, offset);

            Vector2[] diagonalDirs = new Vector2[4];
            diagonalDirs[0] = (collider.GetTopLeft() - collider.offset).normalized * sqrt;
            diagonalDirs[1] = (collider.GetTopRight() - collider.offset).normalized * sqrt;
            diagonalDirs[2] = (collider.GetBottomLeft() - collider.offset).normalized * sqrt;
            diagonalDirs[3] = (collider.GetBottomRight() - collider.offset).normalized * sqrt;
            return diagonalDirs;
        }

        /// <summary> 計算對角線 </summary>
        private static float DiagonalLength(Vector2 size)
        {
            var result = VectorHelper.Distance(Vector2.zero, size);
            return result;
        }

    #endregion

    #region ========== [Private Methods] ==========

        private static Vector2 GetCenter(this BoxCollider2D collider)
        {
            return collider.bounds.center;
        }

    #endregion
    }
}