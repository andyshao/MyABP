using System.Reflection;

namespace MyABP.Dependency
{
    /// <summary>
    ///用于在依赖注入时参数的传递 
    /// </summary>
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        /// 获取要注入的组件
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// 注入配置项
        /// </summary>
        ConventionalRegistrationConfig Config { get; }

    }
}