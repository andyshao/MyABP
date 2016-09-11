using System;

namespace MyABP.Dependency
{
    /// <summary>
    /// 实现该接口以实现依赖解析
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 从IOC容器中获取一个对象
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要获取的对象</typeparam>
        /// <returns>要获取的对象的实例</returns>
        T Resolve<T>();

        /// <summary>
        /// 从IOC容器中获取一个对象
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要转换为的对象类型</typeparam>
        /// <param name="type">要解析的对象类型</param>
        /// <returns>对象的实例</returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// 从IOC容器中获取一个对象
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要获取的对象</typeparam>
        /// <param name="argumentAsAnonymousType">构造方法参数</param>
        /// <returns>对象的实例</returns>
        T Resovle<T>(object argumentAsAnonymousType);

        /// <summary>
        /// 从IOC容器中获取一个对象
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <param name="type">要获取的对象</param>
        /// <returns>对象的实例</returns>
        object Resolve(Type type);

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <param name="type">要获取的对象</param>
        /// <param name="argumentAsAnonymousType">构造方法参数</param>
        /// <returns>对象的实例</returns>
        object Resolve(Type type,object argumentAsAnonymousType);

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要解析的对象</typeparam>
        /// <returns>对象实例集合</returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要解析的对象</typeparam>
        /// <param name="argumentAsAnonymousType">构造方法参数</param>
        /// <returns>对象实例集合</returns>
        T[] ResolveAll<T>(object argumentAsAnonymousType);

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// </summary>
        /// <param name="type">要解析的对象</param>
        /// <returns>对象实例集合</returns>
        object[] ResolveAll(Type type);

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// </summary>
        /// <param name="type">要解析的对象</param>
        /// <param name="argumentAsAnonymousType">构造方法参数</param>
        /// <returns>对象实例集合</returns>
        object[] ResolveAll(Type type, object argumentAsAnonymousType);

        /// <summary>
        /// 释放之前解析的对象
        /// </summary>
        /// <param name="obj">将要释放的对象</param>
        void Release(object obj);

        /// <summary>
        /// 检查给定的类型是否已经注册
        /// </summary>
        /// <param name="type">需要检查的类型</param>
        /// <returns>true or false</returns>
        bool IsRegistered(Type type);

        /// <summary>
        /// 检查给定的类型是否已经注册
        /// </summary>
        /// <typeparam name="TType">需要检查的类型</typeparam>
        /// <returns>true or false</returns>
        bool IsRegistered<TType>();
    }
}