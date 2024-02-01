using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace SiberUtility.Tools
{
    /// <summary> 隨機數值工具
    /// <para> (Random Value)</para>
    /// </summary>
    public class RandomHelper
    {
    #region ========== [Public Variables] ==========

        public const int DefaultValue = -999;

        public static bool Log;
        public static bool UseFake;
        public static int  NextRandomIndex;

    #endregion

    #region ========== [Public Methods] ==========

        public static T GetRandomData<T>(List<T> datas)
        {
            var datasCount = datas.Count;
            Assert.AreNotEqual(0, datasCount, "count can not be zero");
            if (datasCount == 0) return default;

            var randomIndex = UseFake ? NextRandomIndex : Random.Range(0, datasCount);
            return datas[randomIndex];
        }

        /// <summary> 是否小於 Random(1 ~ Max)? </summary>
        public static bool GetRandomResult(int rate, int max)
        {
            var randomValue = GetRandomValue(max);
            return GetRPNGResult(randomValue, rate);
        }

        /// <summary>
        ///     start from 1 to max (include)
        /// </summary>
        /// <param name="max">include</param>
        /// <returns></returns>
        public static int GetRandomValue(int max)
        {
            return GetRandomValue(1, max);
        }

        /// <summary> 回傳 Min~Max(包含) 之間的隨機值 </summary>
        public static int GetRandomValue(int min, int max)
        {
            if (UseFake) return NextRandomIndex;
            var minValue    = min >= max ? max : min;
            var maxValue    = max + 1;
            var randomValue = Random.Range(minValue, maxValue);
            return randomValue;
        }

        /// <summary> 計算圓桌數值 </summary>
        /// <param name="roundTables"></param>
        /// <param name="weightValue">Weight不可為0，不可大於TotalWeight</param>
        /// <exception cref="Exception">輸入值違反時會丟Exception</exception>
        public static T GetRoundTableValue<T>(List<RoundTable<T>> roundTables, int weightValue = DefaultValue)
        {
            var totalWeight = roundTables.Sum(table => table.weight);
            if (roundTables == null)
                throw new Exception("roundTables count is null");
            if (roundTables.Count == 0)
                throw new Exception("roundTables count is 0");
            if (weightValue <= 0 && weightValue != DefaultValue)
                throw new Exception($"Wrong weight value small than min value, {weightValue}");
            if (weightValue > totalWeight)
                throw new Exception(
                                    $"Wrong weight value big than max value, {weightValue} , Total weight is {totalWeight}");
            // Is normal path or unit test path
            var randomWeightValue = weightValue == DefaultValue ? GetRandomValue(totalWeight) : weightValue;
            T   result            = default;
            if (Log) Debug.Log($"[GetRoundTableValue] weightValue : {weightValue}");
            for (var index = 0; index < roundTables.Count; index++)
            {
                var roundTable     = roundTables[index];
                var weight         = roundTable.weight;
                var subtractResult = randomWeightValue - weight;
                if (Log)
                    Debug.Log($"index: {index} , weight : {weight} , value : {roundTable.value} " +
                              $"totalWeight : {randomWeightValue} , Subtract result : {subtractResult}");
                randomWeightValue = subtractResult;
                if (randomWeightValue <= 0)
                {
                    result = roundTable.value;
                    break;
                }
            }

            return result;
        }

        public static bool GetRPNGResult(int randomValue, int rate)
        {
            return randomValue <= rate;
        }

        /// <summary> 獲得視窗外的隨機位置 </summary>
        /// 螢幕座標的概念： 四個點
        /// (0,1080) (1920,1080)
        /// (0,0) (1920,0)
        /// <param name="mainCamera"> 指定相機 </param>
        public static Vector2 GetRandomScreenOutPos(Camera mainCamera)
        {
            var screenWidth  = Screen.width;
            var screenHeight = Screen.height;

            float offset       = 1.5f;
            int   randomEdge   = Random.Range(0, 4);
            var   randomWidth  = Random.Range(0, screenWidth);  // 隨機 寬
            var   randomHeight = Random.Range(0, screenHeight); // 隨機 高

            Vector2 spawnPosition = randomEdge switch
            {
                0 => GetWorldPos(mainCamera, randomWidth, screenHeight) + Vector2.up * offset,    //上
                1 => GetWorldPos(mainCamera, randomWidth, 0) + Vector2.down * offset,             //下
                2 => GetWorldPos(mainCamera, 0, randomHeight) + Vector2.left * offset,            //左
                3 => GetWorldPos(mainCamera, screenWidth, randomHeight) + Vector2.right * offset, //右
                _ => Vector2.zero
            };
            return spawnPosition;
        }

        /// <summary> 獲得圓周上的隨機位置 </summary>
        /// <param name="radius"> 半徑 </param>
        public static Vector2 GetRandomPosOnCircle(float radius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f); // 0 到 2π 的随机角度
            float x     = Mathf.Cos(angle) * radius;
            float y     = Mathf.Sin(angle) * radius;

            return new Vector2(x, y);
        }

        private static Vector2 GetWorldPos(Camera mainCamera, int randomWidth, int screenHeight)
        {
            var position = new Vector2(randomWidth, screenHeight);
            return mainCamera.ScreenToWorldPoint(position);
        }

        /// <summary> 骰 0~100% 真實機率</summary>
        public static int DiceProbability()
        {
            return Random.Range(0, 101);
        }

    #endregion
    }

    [Serializable]
    public class RoundTable<T>
    {
        public int  weight;
        public T    value;
        public void AddWeight(int weight) => this.weight += weight;
    }
}