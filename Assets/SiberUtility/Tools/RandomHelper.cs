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