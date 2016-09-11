using System;

namespace MyABP.Modules
{
    /// <summary>
    /// 用来指定依赖的模块
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// 依赖的模块类型
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }

        /// <summary>
        ///用来指定依赖的模块
        /// </summary>
        /// <param name="dependedModuleTypes">Types of depended modules</param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}