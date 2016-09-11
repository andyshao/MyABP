using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core.Logging;
using MyABP.Collections.Extensions;
using MyABP.Configuration.Startup;
using MyABP.Dependency;

namespace MyABP.Modules
{
    /// <summary>
    /// 所有的模块定义类必须继承此类
    /// </summary>
    /// <remarks>
    /// 一个模块通常定义在它自己所处的模块中；
    /// 在应用程序启动和关闭时实现一些操作在模块事件中；
    /// 同时也可以定义依赖的模块
    /// </remarks>
    public abstract class AbpModule
    {
        /// <summary>
        /// 获取<see cref="IocManager"/>的一个引用
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        /// 获取<see cref="IAbpStartupConfiguration"/>的一个引用
        /// </summary>
        protected internal IAbpStartupConfiguration Configuration { get; internal set; }

        /// <summary>
        /// 日志操作类
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 应用程序启动第一调用，这里面的代码会在依赖注入之前调用
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// 一般用来进行注册这个模块的依赖到Ioc容器中
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 应用程序启动最后执行
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 当应用程序关闭时调用
        /// </summary>
        public virtual void Shutdown()
        {

        }

        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }

        /// <summary>
        /// 检查给定的类型是否为abp模块类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAbpModule(Type type)
        {
            return type.IsClass &&
                   !type.IsAbstract &&
                   !type.IsGenericType &&
                   typeof(AbpModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// 查找一个模块的依赖模块
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsAbpModule(moduleType))
            {
                throw new AbpInitializationException("This type is not an ABP module: " +
                                                     moduleType.AssemblyQualifiedName);
            }

            var list =new List<Type>();

            if (moduleType.IsDefined(typeof(DependsOnAttribute),true))
            {
                var dependsOnAttributes =
                    moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取模块依赖的模块（并递归获取依赖模块的依赖）
        /// </summary>
        public static List<Type> FindDependedModuleTypesRecursively(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesResursively(list, moduleType);
            list.AddIfNotContains(typeof(AbpKernelModule));
            return list;
        }

        /// <summary>
        /// 递归添加模块和模块的依赖到集合中
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="module"></param>
        private static void AddModuleAndDependenciesResursively(List<Type> modules, Type module)
        {
            if (!IsAbpModule(module))
            {
                throw new AbpInitializationException("This type is not an ABP module: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesResursively(modules, dependedModule);
            }
        }

    }
}