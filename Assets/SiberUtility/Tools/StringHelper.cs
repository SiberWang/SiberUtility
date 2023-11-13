using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SiberUtility.Tools
{
    public static class StringHelper
    {
        /// <summary> 取出 / 與 / 之間的參數 </summary>
        /// <example> Main/TagA/Player_01 <br/>
        /// 拿到StringList (Main , TagA , Player_01) </example>>
        /// <param name="path"> 路徑字串 </param>
        /// <returns> list </returns>
        public static List<string> GetPathSplit(string path)
        {
            return path.Split('/').ToList();
        }

        public static string GetFullPath(List<string> splitList)
        {
            var resultPath = "";
            for (var i = 0; i < splitList.Count; i++)
            {
                var slash = i == splitList.Count - 1 ? string.Empty : "/";
                resultPath += splitList[i] + slash;
            }

            Debug.Log($"path :{resultPath}");
            return resultPath;
        }
    }
}