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

        public static bool EnableLog   = true;
        public static bool IsEditorLog = true;

        public static void SaveByJson(string fileName, object data)
        {
            var json = JsonUtility.ToJson(data);
            var path = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                File.WriteAllText(path, json);
            #if UNITY_EDITOR
                Debug.Log($"成功存取 Json 檔案到: {path}");
            #endif
            }
            catch (Exception exception)
            {
            #if UNITY_EDITOR
                Debug.LogError($"儲存 Json 檔案失敗: {path} , {exception}");
            #endif
            }
        }

        public static T LoadFromJson<T>(string fileName)
        {
            var path = Path.Combine(Application.persistentDataPath, fileName);
            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);
            #if UNITY_EDITOR
                Debug.Log("成功讀取 Json 檔案");
            #endif
                return data;
            }
            catch (Exception exception)
            {
            #if UNITY_EDITOR
                Debug.LogError($"讀取 Json 檔案失敗: {path} , {exception}");
            #endif
                return default;
            }
        }

        public static void DeleteJson(string fileName)
        {
            var path = Path.Combine(Application.persistentDataPath, fileName);
            if (!File.Exists(path))
            {
            #if UNITY_EDITOR
                Debug.Log("已經沒有 Json 檔案可以刪除");
            #endif
                return;
            }

            File.Delete(path);
        #if UNITY_EDITOR
            Debug.Log("成功刪除 Json 檔案");
        #endif
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

        private static void ShowLog(string message)
        {
            if (!EnableLog) return;
            if (IsEditorLog)
            {
            #if UNITY_EDITOR
                Debug.Log(message);
            #endif
            }
            else
            {
                Debug.Log(message);
            }
        }

        private static void ShowErrorLog(string message)
        {
            if (!EnableLog) return;
            if (IsEditorLog)
            {
            #if UNITY_EDITOR
                Debug.LogError(message);
            #endif
            }
            else
            {
                Debug.LogError(message);
            }
        }

    #endregion
    }
}