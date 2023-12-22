using System;
using System.Collections.Generic;

namespace SiberUtility.Tools.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>Perform an action on each item.</summary>
        /// <param name="source">The source.</param>
        /// <param name="action">The action to perform.</param>
        public static IEnumerable<T> DoForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T obj in source) action(obj);
            return source;
        }
    }
}