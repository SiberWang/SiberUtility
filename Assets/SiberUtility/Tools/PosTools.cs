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
                    if (tempCount <= 0) return spawnPosList;
                }
            }

            return spawnPosList;
        }

        // 修改回傳型別：List<Vector2> -> List<List<Vector2>>
        public static List<List<Vector2>> GetProgressiveCircleLists
            (Vector2 originPos, int posCount, float radius = 0.5f, float offset = 0.5f, int unit = 5)
        {
            // 大清單 (裝每一圈)
            List<List<Vector2>> allRings = new List<List<Vector2>>();

            var total     = Mathf.CeilToInt((float)posCount / (float)unit);
            var tempCount = posCount;

            if (total <= 0) return allRings;

            for (int a = 0; a < total; a++)
            {
                // 小清單 (裝這一圈的點)
                List<Vector2> currentRing = new List<Vector2>();

                var spawnUnit = unit * (a + 1);

                for (int b = 0; b < spawnUnit; b++)
                {
                    var angle             = b * Mathf.PI * 2f / spawnUnit;
                    var spacing           = radius * offset * a;
                    var addCircleSpawnPos = originPos + AddCircleSpawnPos(radius, angle, spacing);

                    currentRing.Add(addCircleSpawnPos); // 加入小清單

                    tempCount--;
                    if (tempCount <= 0)
                    {
                        allRings.Add(currentRing); // 提早結束也要把最後一圈加進去
                        return allRings;
                    }
                }

                // 這一圈跑完，加入大清單
                allRings.Add(currentRing);
            }

            return allRings;
        }

        /// <summary>
        /// 指定圈數生成位置 (每圈數量遞增)
        /// </summary>
        /// <param name="originPos">中心點</param>
        /// <param name="startUnit">起始單位 (第一圈幾顆，ex: 5)</param>
        /// <param name="ringCount">總共要幾圈 (ex: 4)</param>
        /// <param name="radius">基礎半徑</param>
        /// <param name="offset">圈與圈的間距係數</param>
        public static List<List<Vector2>> GetMultiRingList
        (
            Vector2 originPos,
            int     startUnit,
            int     ringCount,
            float   radius = 0.5f,
            float   offset = 0.5f)
        {
            List<List<Vector2>> allRings = new List<List<Vector2>>();

            // 外層迴圈：跑指定的圈數 (例如 4 圈)
            for (int r = 0; r < ringCount; r++)
            {
                List<Vector2> currentRing = new List<Vector2>();

                // 計算這一圈要幾顆：
                // r=0 -> 5 * 1 = 5
                // r=1 -> 5 * 2 = 10
                // r=2 -> 5 * 3 = 15
                int countInThisRing = startUnit * (r + 1);

                // 計算這一圈的半徑間距 (spacing)
                // r=0 -> 間距 0
                // r=1 -> 間距 radius * offset
                float spacing = radius * offset * r;

                // 內層迴圈：生成這一圈的點
                for (int i = 0; i < countInThisRing; i++)
                {
                    // 角度平均分配
                    float angle = i * Mathf.PI * 2f / countInThisRing;

                    // 計算座標 (沿用你原本的 helper 方法邏輯)
                    Vector2 posOffset = AddCircleSpawnPos(radius, angle, spacing);

                    currentRing.Add(originPos + posOffset);
                }

                allRings.Add(currentRing);
            }

            return allRings;
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