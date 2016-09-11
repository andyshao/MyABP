using System;
using System.Collections.Generic;

namespace MyABP.Modules
{
    /// <summary>
    /// Abp 模块管理
    /// </summary>
    public interface IAbpModuleManager
    {
        AbpModuleInfo StartupModule { get; }
        IReadOnlyList<AbpModuleInfo> Modules { get; }

        /// <summary>
        /// 初始化模块
        /// 主要进行模块的依赖注入
        /// </summary>
        /// <param name="startupModulle"></param>
        void Initialize(Type startupModulle);

        /// <summary>
        /// 启动模块
        /// 主要是分别调用模块的PreInitialize,Initialize,PostInitialize方法
        /// </summary>
        void StartModules();

        /// <summary>
        /// 关闭模块
        /// </summary>
        void ShutdownModules();
    }
}