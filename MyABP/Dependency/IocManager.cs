using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace MyABP.Dependency
{
    /// <summary>
    /// 这个类用于直接执行依赖注入
    /// </summary>
    public class IocManager : IIocManager
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        public static IocManager Instance { get; private set; }

        /// <summary>
        /// Castle windsor Ioc 容器
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        /// <summary>
        /// 需要注册的约定注册列表
        /// </summary>
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        static IocManager()
        {
            Instance = new IocManager();
        }

        /// <summary>
        /// 创建一个新的 <see cref="IocManager"/> 对象.
        /// 一般来说, 不需要直接实例化一个 <see cref="IocManager"/>对象.
        /// 这对于测试来说会有帮助
        /// </summary>
        public IocManager()
        {
            IocContainer = new WindsorContainer();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();

            //注册自己
            IocContainer.Register(
                Component.For<IocManager, IIocManager, IIocRegistrar, IIocResolver>().UsingFactoryMethod(() => this)
                );
        }

        /// <summary>
        /// 添加一个依赖注册器用来约定注册
        /// </summary>
        /// <param name="registrar">依赖注册器</param>
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        /// <summary>
        /// 通过所有约定的注册器，注册指定的程序集的类型,查看方法<see cref="IocManager.AddConventionalRegistrar"/>
        /// </summary>
        /// <param name="assembly">将要注册的程序集</param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            RegisterAssemblyByConvention(assembly, new ConventionalRegistrationConfig());
        }

        /// <summary>
        /// 通过所有约定的注册器，注册指定的程序集的类型,查看方法<see cref="IocManager.AddConventionalRegistrar"/>
        /// </summary>
        /// <param name="assembly">将要注册的程序集</param>
        /// <param name="config">额外的配置项</param>
        public void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config)
        {
            var context = new ConventionalRegistrationContext(this, config, assembly);
            foreach (var registrar in _conventionalRegistrars)
            {
                registrar.RegisterAssembly(context);
            }

            if (config.InstallInstallers)
            {
                IocContainer.Install(FromAssembly.Instance(assembly));
            }
        }

        /// <summary>
        /// 注册指定的类型
        /// </summary>
        /// <typeparam name="T">注册类的类型</typeparam>
        /// <param name="lifeStyle">对象的生命周期</param>
        public void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class
        {
            IocContainer.Register(ApplyLifeStyle(Component.For<T>(), lifeStyle));
        }

        /// <summary>
        /// 根据指定对象的生命周期和类型注册
        /// </summary>
        /// <param name="type">注册类的类型</param>
        /// <param name="lifeStyle">对象的生命周期</param>
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifeStyle(Component.For(type), lifeStyle));
        }

        /// <summary>
        /// 注册类型根据指定的实现方式
        /// </summary>
        /// <param name="type">注册类的类型</param>
        /// <param name="impl">注册类型的实现</param>
        /// <param name="lifeStyle">对象的生命周期</param>
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifeStyle(Component.For(type, impl), lifeStyle));
        }

        /// <summary>
        /// 注册类型根据指定的实现方式
        /// </summary>
        /// <typeparam name="TType">注册类的类型</typeparam>
        /// <typeparam name="TImpl">注册类型的实现</typeparam>
        /// <param name="lifeStyle">对象的生命周期</param>
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifeStyle(Component.For<TType, TImpl>(), lifeStyle));
        }

        /// <summary>
        /// 检查指定的类型是否已经注册
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <returns>true or false</returns>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        /// <summary>
        /// 检查指定的类型是否已经注册
        /// </summary>
        /// <typeparam name="T">要检查的类型</typeparam>
        /// <returns>true or false</returns>
        public bool IsRegistered<T>()
        {
            return IocContainer.Kernel.HasComponent(typeof(T));
        }

        /// <summary>
        /// 从IOC容器中获取一个对象
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要获取的对象</typeparam>
        /// <returns>要获取的对象的实例</returns>
        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        /// <summary>
        /// 从IOC容器中获取一个对象
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要转换为的对象类型</typeparam>
        /// <param name="type">要解析的对象类型</param>
        /// <returns>对象的实例</returns>
        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        /// <summary>
        /// 从IOC容器中获取一个对象
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要获取的对象</typeparam>
        /// <param name="argumentAsAnonymousType">构造方法参数</param>
        /// <returns>对象的实例</returns>
        public T Resovle<T>(object argumentAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentAsAnonymousType);
        }

        /// <summary>
        /// 从IOC容器中获取一个对象
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <param name="type">要获取的对象</param>
        /// <returns>对象的实例</returns>
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <param name="type">要获取的对象</param>
        /// <param name="argumentAsAnonymousType">构造方法参数</param>
        /// <returns>对象的实例</returns>
        public object Resolve(Type type, object argumentAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentAsAnonymousType);
        }

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要解析的对象</typeparam>
        /// <returns>对象实例集合</returns>
        public T[] ResolveAll<T>()
        {
            return IocContainer.ResolveAll<T>();
        }

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// 返回的对象在使用后必须释放
        /// </summary>
        /// <typeparam name="T">要解析的对象</typeparam>
        /// <param name="argumentAsAnonymousType">构造方法参数</param>
        /// <returns>对象实例集合</returns>
        public T[] ResolveAll<T>(object argumentAsAnonymousType)
        {
            return IocContainer.ResolveAll<T>(argumentAsAnonymousType);
        }

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// </summary>
        /// <param name="type">要解析的对象</param>
        /// <returns>对象实例集合</returns>
        public object[] ResolveAll(Type type)
        {
            return IocContainer.ResolveAll(type).Cast<object>().ToArray();
        }

        /// <summary>
        /// 获取指定类型的所有的实现方式
        /// </summary>
        /// <param name="type">要解析的对象</param>
        /// <param name="argumentAsAnonymousType">构造方法参数</param>
        /// <returns>对象实例集合</returns>
        public object[] ResolveAll(Type type, object argumentAsAnonymousType)
        {
            return IocContainer.ResolveAll(type, argumentAsAnonymousType).Cast<object>().ToArray();
        }

        /// <summary>
        /// 释放之前解析的对象
        /// </summary>
        /// <param name="obj">将要释放的对象</param>
        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        /// <summary>释放</summary>
        public void Dispose()
        {
            IocContainer.Dispose();
        }

        /// <summary>
        /// 应用生命周期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registration"></param>
        /// <param name="lifeStyle"></param>
        /// <returns></returns>
        private static ComponentRegistration<T> ApplyLifeStyle<T>(ComponentRegistration<T> registration,
            DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
    }
}