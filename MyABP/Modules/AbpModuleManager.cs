using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using MyABP.Collections.Extensions;
using MyABP.Configuration.Startup;
using MyABP.Dependency;
using MyABP.PlugIns;

namespace MyABP.Modules
{
    /// <summary>
    /// Abpģ�������
    /// </summary>
    public class AbpModuleManager : IAbpModuleManager
    {
        public AbpModuleInfo StartupModule { get; private set; }

        public ILogger Logger { get; set; }

        private Type _startupModuleType;

        private readonly IIocManager _iocManager;
        private readonly IAbpPlugInManager _abpPlugInManager;
        private readonly AbpModuleCollection _modules;

        public AbpModuleManager(IIocManager iocManager, IAbpPlugInManager abpPlugInManager)
        {
            _iocManager = iocManager;
            _abpPlugInManager = abpPlugInManager;
            _modules = new AbpModuleCollection();
            Logger = NullLogger.Instance;
        }

        public IReadOnlyList<AbpModuleInfo> Modules => _modules;

        /// <summary>
        /// ��ʼ��ģ��
        /// ��Ҫ����ģ�������ע��
        /// </summary>
        /// <param name="startupModulle"></param>
        public void Initialize(Type startupModulle)
        {
            _startupModuleType = startupModulle;
            LoadAllModules();
        }

        /// <summary>
        /// ����ģ��
        /// ��Ҫ�Ƿֱ����ģ���PreInitialize,Initialize,PostInitialize����
        /// </summary>
        public void StartModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module=>module.Instance.PreInitialize());
            sortedModules.ForEach(module=>module.Instance.Initialize());
            sortedModules.ForEach(module=>module.Instance.PostInitialize());
        }

        /// <summary>
        /// �ر�ģ��
        /// </summary>
        public void ShutdownModules()
        {
            Logger.Debug("Shutting down has been started");

            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());

            Logger.Debug("Shutting down completed.");
        }

        #region ˽�з���

        /// <summary>
        /// �������е�ģ��
        /// </summary>
        private void LoadAllModules()
        {
            Logger.Debug("Loading Abp modules...");

            //1���ҵ�����ģ��
            var moduleTypes = FindAllModules();

            Logger.Debug("Found " + moduleTypes.Count + " Abp modules in total.");
            
            //2��������ģ�鶼ע�ᵽIOC������
            RegisterModules(moduleTypes);
            //3����Ioc���������ν���ģ��
            CreateModules(moduleTypes);
            //4�������е�model��������ȷ������ģ��AbpKernelModule�ڵ�һ��λ��
            AbpModuleCollection.EnsureKernelModuleToBeFirst(_modules);
            //5������������ģ�������ģ��
            SetDependencies();

            Logger.DebugFormat("{0} Modules loaded.", _modules.Count);
        }

        /// <summary>
        /// ����ģ�������ģ��
        /// </summary>
        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                moduleInfo.Dependencies.Clear();

                foreach (var dependedModuleType in AbpModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new AbpInitializationException("Could not find a depended module "
                                                             + dependedModuleType.AssemblyQualifiedName + " for" +
                                                             moduleInfo.Type);
                    }

                    var firstDependency = moduleInfo.Dependencies.FirstOrDefault(md => md.Type == dependedModuleType);
                    if (firstDependency == null)
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }

        /// <summary>
        /// ����ģ��
        /// </summary>
        /// <param name="moduleTypes"></param>
        private void CreateModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                var moduleObj = _iocManager.Resolve(moduleType) as AbpModule;
                if (moduleObj == null)
                {
                    throw new AbpInitializationException("This type is not an Abp module: " + moduleType.AssemblyQualifiedName);
                }

                moduleObj.IocManager = _iocManager;
                moduleObj.Configuration = _iocManager.Resolve<IAbpStartupConfiguration>();

                var moduleInfo = new AbpModuleInfo(moduleType, moduleObj);
                _modules.Add(moduleInfo);

                if (moduleType == _startupModuleType)
                {
                    StartupModule = moduleInfo;
                }

                Logger.Debug("Loaded Module: " + moduleType.AssemblyQualifiedName);
            }
        }

        /// <summary>
        /// ע��ģ�鵽Ioc����
        /// </summary>
        /// <param name="moduleTypes"></param>
        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }

        /// <summary>
        /// ��ȡ���е�ģ��
        /// </summary>
        /// <returns></returns>
        private List<Type> FindAllModules()
        {
            var modules = AbpModule.FindDependedModuleTypesRecursively(_startupModuleType);
            AddPlugInModules(modules);
            return modules;
        }

        /// <summary>
        /// ������еĲ��ģ��
        /// </summary>
        /// <param name="modules"></param>
        private void AddPlugInModules(List<Type> modules)
        {
            foreach (var plugInSource in _abpPlugInManager.PlugInSources)
            {
                foreach (var module in plugInSource.GetModules())
                {
                    modules.AddIfNotContains(module);
                }
            }
        }
        
        #endregion
    }
}