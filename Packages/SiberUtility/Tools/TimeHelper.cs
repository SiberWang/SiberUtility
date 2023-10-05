using System;

namespace SiberUtility.Tools
{
    public static class TimeHelper
    {
    #region ========== [Public Methods] ==========

        /// <summary> 計時：時間到 MaxTime 就 true </summary>
        /// <param name="time"> 計算的時間 </param>
        /// <param name="maxTime"> 目標時間 </param>
        /// <param name="deltaTime"> 經過時間 (ex: Time.DeltaTime) </param>
        public static bool Timing(ref float time, float maxTime, float deltaTime)
        {
            time += deltaTime;
            if (!(time >= maxTime)) return false;
            time = 0f;
            return true;

        }

        /// <summary> 計時：時間到 MaxTime 就完成 </summary>
        /// <param name="time"> 計算的時間 </param>
        /// <param name="maxTime"> 目標時間 </param>
        /// <param name="deltaTime"> 經過時間 (ex: Time.DeltaTime) </param>
        /// <param name="onCompleted"> 完成事件 </param>
        public static void Timing(ref float time, float maxTime, float deltaTime, Action onCompleted)
        {
            time += deltaTime;
            if (!(time >= maxTime)) return;
            time = 0f;
            onCompleted?.Invoke();
        }

        /// <summary> 倒計時：時間到 0 就 true </summary>
        /// <param name="time"> 計算的時間 </param>
        /// <param name="maxTime"> 目標時間 </param>
        /// <param name="deltaTime"> 經過時間 (ex: Time.DeltaTime) </param>
        public static bool CountDown(ref float time, float maxTime, float deltaTime)
        {
            time -= deltaTime;
            if (time < 0)
            {
                time = maxTime;
                return true;
            }

            return false;
        }

        /// <summary> 轉為 DDD-HH-MM-SS(-FF) </summary>
        public static string DisplayDayTime(TimeSpan currentTime, bool showMilliseconds = false)
        {
            var days  = currentTime.Days > 0 ? currentTime.Days.ToString() : "00";
            var hours = currentTime.Hours > 0 ? currentTime.Hours.ToString() : "00";
            return $"{days}:{hours}:{MinutesTime(currentTime, showMilliseconds)}";
        }

        /// <summary> 轉為 HH-MM-SS(-FF) </summary>
        public static string DisplayHourTime(TimeSpan currentTime, bool showMilliseconds = false)
        {
            var timeHours = currentTime.Days * 24 + currentTime.Hours;
            var hours = timeHours switch
            {
                > 0 and < 10 => $"0{timeHours}",
                > 10         => $"{timeHours}",
                _            => "00"
            };

            return $"{hours}:{MinutesTime(currentTime, showMilliseconds)}";
        }

        /// <summary> 轉為 MM-SS(-FF) </summary>
        public static string MinutesTime(TimeSpan currentTime, bool showMilliseconds = false)
        {
            var minutes = showMilliseconds ? $@"{currentTime:mm\:ss\:ff}" : $@"{currentTime:mm\:ss}";
            return $"{minutes}";
        }

    #endregion
    }
}