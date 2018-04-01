﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DeepCopier
{
    /// <summary>
    /// 工具类
    /// </summary>
    internal static class Utils
    {
        private static Type _typeString = typeof(string);

        private static Type _typeIEnumerable = typeof(IEnumerable);

        private static ConcurrentDictionary<Type, Func<object>> _ctors = new ConcurrentDictionary<Type, Func<object>>();

        /// <summary>
        /// 判断是否是string以外的引用类型
        /// </summary>
        /// <returns>True：是string以外的引用类型，False：不是string以外的引用类型</returns>
        public static bool IsRefTypeExceptString(Type type)
            => !type.IsValueType && type != _typeString;

        /// <summary>
        /// 判断是否是string以外的可遍历类型
        /// </summary>
        /// <returns>True：是string以外的可遍历类型，False：不是string以外的可遍历类型</returns>
        public static bool IsIEnumerableExceptString(Type type)
            => _typeIEnumerable.IsAssignableFrom(type) && type != _typeString;

        /// <summary>
        /// 创建指定类型实例
        /// </summary>
        /// <param name="type">类型信息</param>
        /// <returns>指定类型的实例</returns>
        public static object CreateNewInstance(Type type) =>
            _ctors.GetOrAdd(type,
               t => Expression.Lambda<Func<object>>(Expression.New(t)).Compile())();
    }
}
