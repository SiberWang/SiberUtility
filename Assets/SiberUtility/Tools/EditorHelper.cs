using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace SiberUtility.Tools
{
    public class EditorHelper
    {
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

        /// <summary> 找資料 (可篩選) </summary>
        /// <param name="assetName"> 檔案名稱 </param>
        /// <param name="filter"> 篩選 ex: t:script</param>
        /// <param name="T"> 篩選也會篩指定的 type </param>
        public static List<T> GetAssets<T>(string assetName, string filter = "") where T : Object
        {
            var result = new List<T>();
        #if UNITY_EDITOR
            var guids = AssetDatabase.FindAssets($"{assetName} {filter} t:{typeof(T).Name}");
            if (guids.Length <= 0) Debug.LogError($"Can't find target : [{assetName}]");
            foreach (var guid in guids)
            {
                var assetPath       = AssetDatabase.GUIDToAssetPath(guid);
                var loadAssetAtPath = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                result.Add(loadAssetAtPath);
            }
        #endif
            return result;
        }

        /// <summary> 找單一資料 (可篩選) </summary>
        /// <param name="assetName"> 檔案名稱 </param>
        /// <param name="filter"> 篩選 ex: t:script</param>
        /// <param name="T"> 篩選也會篩指定的 type </param>
        public static T GetAsset<T>(string assetName, string filter = "") where T : Object
        {
            return GetAssets<T>(assetName, filter).FirstOrDefault(asset => asset.name.Equals(assetName));
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

        /// <summary> 全域搜尋 Type By Name </summary>
        /// <param name="typeName"> 指定類別名稱 </param>
        /// <param name="nameSpace"> 可指定 nameSpace , 使其更快搜到 </param>
        public static Type GetTypeByName(string typeName, string nameSpace = "")
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types      = assemblies.SelectMany(a => a.GetTypes());

            Type result;
            if (string.IsNullOrEmpty(nameSpace))
            {
                result = types.Where(x => !x.IsAbstract && !x.IsGenericTypeDefinition && x.IsClass)
                              .FirstOrDefault(t => t.Name == typeName);
            }
            else
            {
                result = types.Where(x => !x.IsAbstract && !x.IsGenericTypeDefinition && x.IsClass &&
                                          x.Namespace != null &&
                                          x.Namespace.Equals(nameSpace))
                              .FirstOrDefault(t => t.Name == typeName);
                Assert.IsNotNull(result, $"Search Type is null , 是否路徑找錯了? , namespace: [{nameSpace}]");
            }
            return result;
        }

        /// <summary> 找 XXX.Data , 利用 type string，去創建 有繼承特定腳本的項目 </summary>
        /// <param name="handleType"> 必須是繼承於 T 的腳本名稱</param>
        /// <returns> T = 指定的類別 </returns>
        public static T NewNestedData<T>(string handleType) where T : class
        {
            if (string.IsNullOrEmpty(handleType))
            {
                Debug.LogError($"HandleType is empty");
                return default;
            }

            // 目標：找出A腳本的 A.Data
            var findMain       = GetTypeByName(handleType, typeof(T).Namespace);
            var searchFullName = findMain.FullName + "+Data";
            var resultType     = GetTypeByFullName(searchFullName);
            if (resultType == null)
            {
                Debug.LogError($"類別: [{handleType}], 腳本內並沒有.Data , 完整路徑: [{searchFullName}]");
                return default;
            }

            if (!resultType.IsSubclassOf(typeof(T)))
            {
                Debug.LogError($"指定類別並沒有繼承: [{nameof(T)}] , 請確認!");
                return default;
            }

            var result = Activator.CreateInstance(resultType) as T;
            return result;
        }

        /// <summary> 全域搜尋 Type By FullName </summary>
        /// <param name="fullName"> 完整路徑名稱 (包含 namespace) </param>
        public static Type GetTypeByFullName(string fullName)
        {
            var type = Type.GetType(fullName);
            if (type != null) return type;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var result = assemblies.SelectMany(a => a.GetTypes())
                                   .Where(x => !x.IsAbstract && !x.IsGenericTypeDefinition && x.IsClass)
                                   .FirstOrDefault(t => t.FullName == fullName);
            return result;
        }
    }
}