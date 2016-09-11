using System;
using System.Collections;
using System.Collections.Generic;

namespace MyABP.Collections.Extensions
{
    //集合的扩展方法
    public static class CollectionExtensionns
    {
        /// <summary>
        /// 判断集合为Null或者为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count == 0;
        }

        /// <summary>
        /// 如果集合不存在该项则添加新项到集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source,T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);

            return true;
        }
        
    }
}