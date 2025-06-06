using System;
using System.IO;
using UnityEngine;

namespace SiberUtility.Systems.FileSaves
{
    /// <summary>
    /// 簡單的 Json 儲存方式 (安全性低)
    /// https://youtu.be/1vOuh3_ZXRQ 參照教學製作
    /// 複雜的可參照 https://youtu.be/aUi9aijvpgs
    /// </summary>
    public static class SaveHelper
    {
    #region ========== [Json SaveSystem] ==========

        public static bool EnableLog       = true;
        public static bool EnableEditorLog = true;
        public static bool IsShowLog => EnableLog && (EnableEditorLog || !Application.isEditor);

        public static void SaveByJson(string fileName, object data, string dataPath)
        {
            var json = JsonUtility.ToJson(data, true);
            var path = Path.Combine(dataPath, fileName);

            try
            {
                File.WriteAllText(path, json);
                ShowLog($"成功存取 Json 檔案到: {path}");
            }
            catch (Exception exception)
            {
                ShowErrorLog($"儲存 Json 檔案失敗: {path} , {exception}");
            }
        }

        public static void SaveBase64ByJson(string fileName, object data, string dataPath)
        {
            var json    = JsonUtility.ToJson(data);
            var bytes   = System.Text.Encoding.UTF8.GetBytes(json);
            var encoded = Convert.ToBase64String(bytes);

            var path = Path.Combine(dataPath, fileName);

            try
            {
                File.WriteAllText(path, encoded);
                ShowLog($"成功存取 Json 檔案到: {path}");
            }
            catch (Exception exception)
            {
                ShowErrorLog($"儲存 Json 檔案失敗: {path} , {exception}");
            }
        }

        public static T LoadFromJson<T>(string fileName, string dataPath)
        {
            var path = Path.Combine(dataPath, fileName);
            if (!File.Exists(path))
            {
                ShowErrorLog($"檔案不存在: {path}");
                return default;
            }

            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);
                ShowLog("成功讀取 Json 檔案");
                return data;
            }
            catch (Exception exception)
            {
                ShowErrorLog($"讀取 Json 檔案失敗: {path} , {exception}");
                return default;
            }
        }

        public static T LoadBase64FromJson<T>(string fileName, string dataPath)
        {
            var path = Path.Combine(dataPath, fileName);
            if (!File.Exists(path))
            {
                ShowErrorLog($"檔案不存在: {path}");
                return default;
            }

            try
            {
                var encoded = File.ReadAllText(path);
                var bytes   = Convert.FromBase64String(encoded);
                var json    = System.Text.Encoding.UTF8.GetString(bytes);
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception exception)
            {
                ShowErrorLog($"讀取 Json 檔案失敗: {path} , {exception}");
                return default;
            }
        }

        public static void DeleteJson(string fileName, string dataPath)
        {
            var path = Path.Combine(dataPath, fileName);
            if (!File.Exists(path))
            {
                ShowLog("已經沒有 Json 檔案可以刪除");
                return;
            }

            File.Delete(path);
            ShowLog("成功刪除 Json 檔案");
        }

    #endregion

    #region ========== [PlayerPrefs] ==========

        /// <summary>
        /// WebGL 比較適合 PlayerPrefs
        /// PlayerPrefs教學 https://youtu.be/tci13zedrzw
        /// </summary>
        public static void SaveByPlayerPrefs(string key, object data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
            ShowLog("成功儲存 PlayerPrefs");
        }

        public static T LoadFromPlayerPrefs<T>(string key)
        {
            try
            {
                var json = PlayerPrefs.GetString(key, null);
                var data = JsonUtility.FromJson<T>(json);
                ShowLog("成功讀取 PlayerPrefs 資料");
                return data;
            }
            catch (Exception exception)
            {
                ShowErrorLog($"讀取 PlayerPrefs 失敗: {exception}");
                return default;
            }
        }

        public static void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            ShowLog("成功刪除 PlayerPrefs");
        }

    #endregion

    #region ========== [Private Methods] ==========

        private static void ShowLog(string message)
        {
            if (!IsShowLog) return;
            Debug.Log(message);
        }

        private static void ShowErrorLog(string message)
        {
            if (!IsShowLog) return;
            Debug.LogError(message);
        }

    #endregion
    }
}