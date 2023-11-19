using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SiberUtility.Tools.Extensions
{
    public static class TransformExtensions
    {
    #region ========== [Public Methods] ==========

        public static IEnumerable<Transform> Children(this Transform parent)
        {
            foreach (Transform child in parent)
                yield return child;
        }

        public static void DestroyChildren(this Transform parent)
        {
            parent.PerformActionOnChildren(child => Object.Destroy(child.gameObject));
        }

        public static void DisableChildren(this Transform parent)
        {
            parent.PerformActionOnChildren(child => child.gameObject.SetActive(false));
        }

        public static void EnableChildren(this Transform parent)
        {
            parent.PerformActionOnChildren(child => child.gameObject.SetActive(true));
        }

    #endregion

    #region ========== [Private Methods] ==========

        private static void PerformActionOnChildren(this Transform parent, Action<Transform> action)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
                action(parent.GetChild(i));
        }

    #endregion
    }
}