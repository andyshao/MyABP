using System;
using Castle.Windsor;

namespace MyABP.Dependency
{
    /// <summary>
    /// 该接口用来进行直接执行依赖注入任务
    /// </summary>
    public interface IIocManager:IIocRegistrar,IIocResolver,IDisposable
    {
        /// <summary>
        /// Castle windsor Ioc 容器
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        /// 检查指定的类型是否已经注册
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <returns>true or false</returns>
        new bool IsRegistered(Type type);

        /// <summary>
        /// 检查指定的类型是否已经注册
        /// </summary>
        /// <typeparam name="T">要检查的类型</typeparam>
        /// <returns>true or false</returns>
        new bool IsRegistered<T>();
    }
    
}