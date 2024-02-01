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

        public static void SetScale(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3(x ?? scale.x, y ?? scale.y, z ?? scale.z);
        }

        public static void SetScale(this Transform transform, Vector3 scale)
        {
            transform.localScale = scale;
        }

        public static void SetScale(this Transform transform, Vector2 scale)
        {
            transform.localScale = (Vector3)scale + Vector3.forward;
        }

        public static void SetScaleAll(this Transform transform, float value)
        {
            transform.localScale = Vector3.one * value;
        }

        public static void AddMultScale(this Transform transform, float value)
        {
            transform.localScale *= value;
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