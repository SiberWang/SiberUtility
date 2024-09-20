using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace SiberUtility.Editor
{
    public static class SortSelectedGameObjectsByName
    {
        [MenuItem(ToolPaths.SortGameObjectsByName_Path)]
        private static void SortSelectedGameObjects()
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            if (selectedGameObjects.Length == 0)
            {
                Debug.LogWarning("No GameObjects selected.");
                return;
            }

            // 將 GameObjects 按照名稱中的數字部分進行排序
            Array.Sort(selectedGameObjects, CompareObjectNames);

            for (int i = 0; i < selectedGameObjects.Length; i++)
            {
                Undo.RecordObject(selectedGameObjects[i].transform, "Sort GameObjects by Name");
                selectedGameObjects[i].transform.SetSiblingIndex(i);
            }
        }

        // 比較兩個 GameObject 的名稱，按照名稱中的數字部分進行排序
        private static int CompareObjectNames(GameObject a, GameObject b)
        {
            string nameA = a.name;
            string nameB = b.name;

            // 使用正則表達式提取名稱中的數字部分
            string          pattern  = @"\d+";
            MatchCollection matchesA = Regex.Matches(nameA, pattern);
            MatchCollection matchesB = Regex.Matches(nameB, pattern);

            // 如果其中一個名稱中沒有數字，直接比較名稱的字母部分
            if (matchesA.Count == 0 || matchesB.Count == 0)
            {
                return String.Compare(nameA, nameB);
            }

            // 提取到的第一個數字部分進行比較
            int numberA = int.Parse(matchesA[0].Value);
            int numberB = int.Parse(matchesB[0].Value);

            return numberA.CompareTo(numberB);
        }
    }
}