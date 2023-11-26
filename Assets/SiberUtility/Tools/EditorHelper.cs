using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SiberUtility.Tools
{
    public class EditorHelper
    {
        /// <summary> 找資料 (可篩選) </summary>
        /// <param name="fileName"> 檔案名稱 </param>
        /// <param name="filter"> ex:t:script</param>
        public static Object GetFileAsset(string fileName, string filter = "")
        {
            var guids  = AssetDatabase.FindAssets($"{fileName} {filter}");
            if (guids.Length <= 0)
            {
                Debug.LogError($"Can't find target : [{fileName}]");
                return null;
            }

            var path     = AssetDatabase.GUIDToAssetPath(guids[0]);
            var instance = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            return instance;
        }

        public static string GetFileAssetPath(Type type, string path = "Assets/")
        {
            return GetFileAssetPaths(type, path).First();
        }

        public static string GetFileAssetPath<T>(string path = "Assets/") where T : class
        {
            return GetFileAssetPaths<T>(path).First();
        }

        public static List<string> GetFileAssetPaths(Type type, string path = "Assets/")
        {
            var st = new List<string>();
            if (string.IsNullOrEmpty(path)) return st;
        #if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets($"t:{type}");
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (!assetPath.Contains(path)) continue;
                st.Add(assetPath);
            }
        #endif
            return st;
        }

        public static List<string> GetFileAssetPaths<T>(string path = "Assets/") where T : class
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(path)) return result;
        #if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (!assetPath.Contains(path)) continue;
                result.Add(assetPath);
            }
        #endif
            return result;
        }

        public static ScriptableObject GetScriptableObject(Type type)
        {
            return GetScriptableObjects(type).FirstOrDefault();
        }

        public static T GetScriptableObject<T>() where T : ScriptableObject
        {
            return GetScriptableObjects<T>().FirstOrDefault();
        }

        public static List<ScriptableObject> GetScriptableObjects(Type type)
        {
            var result = new List<ScriptableObject>();
        #if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets($"t:{type}");
            foreach (var guid in guids)
            {
                var assetPath       = AssetDatabase.GUIDToAssetPath(guid);
                var loadAssetAtPath = AssetDatabase.LoadAssetAtPath(assetPath, type);
                result.Add(loadAssetAtPath as ScriptableObject);
            }
        #endif
            return result;
        }

        public static List<T> GetScriptableObjects<T>() where T : ScriptableObject
        {
            var result = new List<T>();
        #if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            foreach (var guid in guids)
            {
                var assetPath       = AssetDatabase.GUIDToAssetPath(guid);
                var loadAssetAtPath = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                result.Add(loadAssetAtPath);
            }
        #endif
            return result;
        }

        public static List<Object> GetAssets(string assetName)
        {
            var result = new List<Object>();
        #if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets(assetName);
            foreach (var guid in guids)
            {
                var assetPath       = AssetDatabase.GUIDToAssetPath(guid);
                var loadAssetAtPath = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                result.Add(loadAssetAtPath);
            }
        #endif
            return result;
        }

        public static void PingObject(Object instance)
        {
        #if UNITY_EDITOR
            EditorGUIUtility.PingObject(instance);
        #endif
        }

        public static void SelectObject(Object instance)
        {
        #if UNITY_EDITOR
            Selection.activeObject = instance;
        #endif
        }
    }
}