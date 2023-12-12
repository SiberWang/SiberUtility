using System.Collections.Generic;
using UnityEngine;

namespace SiberUtility.Tools
{
    public static class PosTools
    {
        /// <summary> 獲得位置清單 - 抓圓形範圍內均分的位置 </summary>
        /// <param name="originPos"> 起始點 </param>
        /// <param name="posCount"> 位置點總數 </param>
        /// <param name="radius"> 圓的半徑 </param>
        /// <param name="offset"> 偏移 (每圈的偏移位置) </param>
        /// <param name="unit"> 單位 (每圈的單位) </param>
        /// <returns> 計算好的 Vector2 清單 </returns>
        public static List<Vector2> GetProgressiveCircleList
            (Vector2 originPos, int posCount, float radius = 0.5f, float offset = 0.5f, int unit = 5)
        {
            List<Vector2> spawnPosList = new List<Vector2>();

            var total     = Mathf.CeilToInt((float)posCount / (float)unit); // 計算執行次數(無條件進位 , 也必須用float去除)
            var tempCount = posCount;                                       // 總次數，用來扣除，扣完就停止生產

            if (total <= 0) return spawnPosList;
            for (int a = 0; a < total; a++)
            {
                var spawnUnit = unit * (a + 1); // 每一圈的生成單位 (ex: 3 , 5 , 7 , 9)
                for (int b = 0; b < spawnUnit; b++)
                {
                    var angle             = b * Mathf.PI * 2f / spawnUnit;
                    var spacing           = radius * offset * a;
                    var addCircleSpawnPos = originPos + AddCircleSpawnPos(radius, angle, spacing);
                    spawnPosList.Add(addCircleSpawnPos);
                    tempCount--;
                    if (tempCount <= 0) break;
                }
            }

            return spawnPosList;
        }

        private static Vector2 AddCircleSpawnPos(float radius, float angle, float spacing)
        {
            var x      = Mathf.Cos(angle) * (radius + spacing);
            var y      = Mathf.Sin(angle) * (radius + spacing);
            var newPos = new Vector3(x, y);
            return newPos;
        }
    }
}