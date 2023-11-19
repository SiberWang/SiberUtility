using UnityEngine;

namespace SiberUtility.Tools.Extensions
{
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            var component             = gameObject.GetComponent<T>();
            if (!component) component = gameObject.AddComponent<T>();
            return component;
        }

        public static T Get<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>();
        }

        public static T Add<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.AddComponent<T>();
        }

        public static T OrNull<T>(this T obj) where T : Object
        {
            return obj ? obj : null;
        }

        public static void DestroyChildren(this GameObject gameObject)
        {
            gameObject.transform.DestroyChildren();
        }

        public static void EnableChildren(this GameObject gameObject)
        {
            gameObject.transform.EnableChildren();
        }

        public static void DisableChildren(this GameObject gameObject)
        {
            gameObject.transform.DisableChildren();
        }
    }
}