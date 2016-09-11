using System.Collections.Generic;
using System.Linq;

namespace MyABP.Modules
{
    /// <summary>
    /// 存储AbpModuleInfo 对象集合
    /// </summary>
    internal class AbpModuleCollection : List<AbpModuleInfo>
    {
        /// <summary>
        /// 获取模块的实例
        /// </summary>
        /// <typeparam name="TModule">要获取的实例</typeparam>
        /// <returns></returns>
        public TModule GetModule<TModule>() where TModule : AbpModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof(TModule));
            if (module == null)
                throw new AbpException("Can not find module for " + typeof(TModule).FullName);
            return (TModule) module.Instance;
        }

        public List<AbpModuleInfo> GetSortedModuleListByDependency()
        {
            return this;
        }


        /// <summary>
        /// 确保核心模块在第一位置，以便最先加载
        /// </summary>
        /// <param name="modules"></param>
        public static void EnsureKernelModuleToBeFirst(List<AbpModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(m => m.Type == typeof(AbpKernelModule));
            if (kernelModuleIndex > 0)
            {
                var kernelModule = modules[kernelModuleIndex];
                modules.RemoveAt(kernelModuleIndex);
                modules.Insert(0, kernelModule);
            }
        }
    }
}