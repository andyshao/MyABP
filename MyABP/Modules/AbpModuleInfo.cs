using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyABP.Modules
{
    /// <summary>
    /// 用于存储模块信息
    /// </summary>
    public class AbpModuleInfo
    {
        /// <summary>
        /// 模块类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 所在程序集
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// 依赖模块
        /// </summary>
        public List<AbpModuleInfo> Dependencies { get; }

        /// <summary>
        /// 模块实例
        /// </summary>
        public AbpModule Instance { get;}

        public AbpModuleInfo(Type type, AbpModule instance)
        {
            Type = type;
            Assembly = type.Assembly;
            Instance = instance;
            Dependencies=new List<AbpModuleInfo>();
        }
        
        public override string ToString()
        {
            return Type.AssemblyQualifiedName ?? Type.FullName;
        }
    }
}