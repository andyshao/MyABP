using System;
using System.Reflection;

namespace MyABP.Dependency
{
    /// <summary>
    /// 定义用于进行依赖注入的接口
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// 添加一个依赖注册器用来约定注册
        /// </summary>
        /// <param name="registrar">依赖注册器</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        /// 通过所有约定的注册器，注册指定的程序集的类型,查看方法<see cref="IocManager.AddConventionalRegistrar"/>
        /// </summary>
        /// <param name="assembly">将要注册的程序集</param>
        void RegisterAssemblyByConvention(Assembly assembly);
        
        /// <summary>
        /// 通过所有约定的注册器，注册指定的程序集的类型,查看方法<see cref="IocManager.AddConventionalRegistrar"/>
        /// </summary>
        /// <param name="assembly">将要注册的程序集</param>
        /// <param name="config">额外的配置项</param>
        void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config);

        /// <summary>
        /// 注册指定的类型
        /// </summary>
        /// <typeparam name="T">注册类的类型</typeparam>
        /// <param name="lifeStyle">对象的生命周期</param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class;

        /// <summary>
        /// 根据指定对象的生命周期和类型注册
        /// </summary>
        /// <param name="type">注册类的类型</param>
        /// <param name="lifeStyle">对象的生命周期</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// 注册类型根据指定的实现方式
        /// </summary>
        /// <param name="type">注册类的类型</param>
        /// <param name="impl">注册类型的实现</param>
        /// <param name="lifeStyle">对象的生命周期</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// 注册类型根据指定的实现方式
        /// </summary>
        /// <typeparam name="TType">注册类的类型</typeparam>
        /// <typeparam name="TImpl">注册类型的实现</typeparam>
        /// <param name="lifeStyle">对象的生命周期</param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

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