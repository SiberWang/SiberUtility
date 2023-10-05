﻿using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace SiberUtility.Tools
{
    public static class MathHelper
    {
    #region ========== [Public Methods] ==========

        public static float Abs(float value)
        {
            return Mathf.Abs(value);
        }

        public static float Clamp(float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        public static int Clamp(int value, int min, int max)
        {
            return Mathf.Clamp(value, min, max);
        }

        public static float Clamp_0_To_Max(float current, float max)
        {
            return Mathf.Clamp(current, 0, max);
        }

        /// <summary>
        ///     max will be float.MaxValue
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static float Clamp_0_To_Max(float current)
        {
            return Mathf.Clamp(current, 0, float.MaxValue);
        }

        /// <summary>
        ///     max will be float.MaxValue
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static int Clamp_0_To_Max(int current)
        {
            return Mathf.Clamp(current, 0, int.MaxValue);
        }

        public static int Clamp_0_To_Max(int current, int max)
        {
            return Mathf.Clamp(current, 0, max);
        }

        public static float Clamp_To_Min(float current, float min)
        {
            return Mathf.Clamp(current, min, current);
        }

        public static int Clamp_To_Min(int current, int min)
        {
            return Mathf.Clamp(current, min, 0);
        }

        public static bool Equal(float a, float b, float precision = 0.01f)
        {
            var diff          = Abs(a) - Abs(b);
            var isInTolerance = precision > Abs(diff);
            return isInTolerance;
        }

        public static float GetRoundPercent(float current, float max, int digits = 3)
        {
            Assert.IsTrue(max >= 0, $"Max {max} is less than zero.");
            var percent      = current / max;
            var roundPercent = GetRoundValue(percent, digits);
            return roundPercent;
        }

        public static float GetRoundValue(float value, int digits)
        {
            var roundPercent = MathF.Round(value, digits, MidpointRounding.AwayFromZero);
            return roundPercent;
        }

        public static int GetRoundValueInt(float value, int digits = 1)
        {
            var roundValue = MathF.Round(value, digits, MidpointRounding.AwayFromZero);
            return (int)roundValue;
        }

        /// <summary> 值是否在Min~Max之間? </summary>
        public static bool IsBetween(float value, float minValue, float maxValue)
        {
            return maxValue >= value && value >= minValue;
        }

        /// <summary> 是負數 </summary>
        public static bool IsNegative(float value)
        {
            return value < 0;
        }

        /// <summary> 可以讓數字在待在 A~B 之間 </summary>
        /// <param name="currentIndex"> 當前的Index </param>
        /// <param name="index"> 看你要 +N 或 -N ? </param>
        /// <param name="totalIndices"> 最大Range </param>
        /// <returns> next index </returns>
        public static int GetSwitchIndex(int currentIndex, int index, int totalIndices)
        {
            if (totalIndices == 0) return 0;
            var switchIndex = (currentIndex + (index % totalIndices) + totalIndices) % totalIndices;
            return switchIndex;
        }

    #endregion
    }
}