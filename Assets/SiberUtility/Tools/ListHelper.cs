using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SiberUtility.Tools
{
    public static class ListHelper
    {
        // 清單深拷貝，用的時候不用改到原先複製的那個值
        public static List<C> CloneListFromOldList<C>(List<C> oldList) where C : class, new()
        {
            var newList = new List<C>(oldList.Count);
            foreach (var oldClass in oldList)
            {
                var stat = new C();
                newList.Add(stat);
            }

            return newList;
        }

        // 清單深拷貝，這個更簡便
        public static List<T> CloneList<T>(List<T> listToClone) where T : IMyCloneable<T>
        {
            return listToClone.Select(c => c.Clone()).ToList();
        }

        /// <summary> 清單深拷貝 </summary>
        public static List<T> DeepCopy<T>(List<T> objectToCopy)
        {
            if (objectToCopy == null)
                return null;

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream    stream    = new MemoryStream();
            formatter.Serialize(stream, objectToCopy);
            stream.Position = 0;
            List<T> copiedObject = (List<T>)formatter.Deserialize(stream);

            return copiedObject;
        }

        /// <summary> Fisher-Yates 洗牌算法 , 來隨機排序項目 </summary>
        public static List<T> ListByFisherYates<T>(List<T> list)
        {
            for (var i = list.Count - 1; i >= 1; i--)
            {
                var j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            return list;
        }

        /// <summary>
        /// 全篇攻略：https://dotblogs.com.tw/tiffany/2015/04/03/150935
        /// https://dotblogs.com.tw/noncoder/2018/06/07/Except
        /// 差集比較 - 拿出不同的單元
        /// mainList = 1 2 3 4 5
        /// targetList = 2 4 6 8 9
        /// mainList 中，與 targetList 差異的資料 : 6 8 9
        /// targetList 中，與 mainList 差異的資料 : 1 3 5
        /// </summary>
        public static List<string> GetExceptList(IEnumerable<string> mainList, IEnumerable<string> targetList)
        {
            // StringComparer.OrdinalIgnoreCase 不比較大小符號
            var newStrings = mainList.Except(targetList, StringComparer.OrdinalIgnoreCase).ToList();
            return newStrings;
        }

        /// <summary>
        /// https://learn.microsoft.com/zh-tw/dotnet/api/system.linq.enumerable.union?view=net-8.0
        /// 聯集比較 - 拿出相同的單元
        /// mainList = 1 2 3 4 5
        /// targetList = 2 4 6 8 9
        /// new list = 1 2 3 4 5 6 8 9
        /// </summary>
        public static List<string> GetUnionList(IEnumerable<string> mainList, IEnumerable<string> targetList)
        {
            var newStrings = mainList.Union(targetList).ToList();
            return newStrings;
        }

        public static List<T> GetExceptListOfType<T>(IEnumerable<T> mainList, IEnumerable<T> targetList)
        {
            var newList = mainList.Except<T>(targetList).ToList();
            return newList;
        }

        public static List<string> SwitchStringsToList(string content, List<string> existContentList)
        {
            if (string.IsNullOrEmpty(content)) return new List<string>();
            var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var finalList = lines.Select(line => line.Replace(" ", ""))
                                 .Where(line => !string.IsNullOrWhiteSpace(line))
                                 .Where(line => !string.IsNullOrEmpty(line))
                                 .Except(existContentList, StringComparer.OrdinalIgnoreCase)
                                 .ToList();
            return finalList;
        }

        public static void AddByNotContain<T>(this List<T> list, T item)
        {
            if (!list.Contains(item))
                list.Add(item);
        }

        public static T[] GetEnumArray<T>() where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }
    }

    public static class DictionaryHelper
    {
        public static void AddByNotContain<TKey, TValue>
            (this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, value);
        }

        public static void AddOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key)) dictionary[key] = value;
            else dictionary.Add(key, value);
        }

        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
            {
                Debug.LogError($"Not ContainsKey: [{key}]");
            }
        }
    }

    public interface IMyCloneable<T>
    {
        T Clone();
    }
}