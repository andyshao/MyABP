using System.Collections.Generic;
using System.Linq;

namespace MyABP.Modules
{
    /// <summary>
    /// �洢AbpModuleInfo ���󼯺�
    /// </summary>
    internal class AbpModuleCollection : List<AbpModuleInfo>
    {
        /// <summary>
        /// ��ȡģ���ʵ��
        /// </summary>
        /// <typeparam name="TModule">Ҫ��ȡ��ʵ��</typeparam>
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
        /// ȷ������ģ���ڵ�һλ�ã��Ա����ȼ���
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