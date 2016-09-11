using System.Reflection;

namespace MyABP.Dependency
{
    /// <summary>
    /// 用于传递注册时需要的对象
    /// </summary>
    public class ConventionalRegistrationContext:IConventionalRegistrationContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocManager"></param>
        /// <param name="config"></param>
        /// <param name="assembly"></param>
        public ConventionalRegistrationContext(IIocManager iocManager, ConventionalRegistrationConfig config, Assembly assembly)
        {
            IocManager = iocManager;
            Config = config;
            Assembly = assembly;
        }

        /// <summary>
        /// 获取要注入的组件
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// 注入配置项
        /// </summary>
        public ConventionalRegistrationConfig Config { get; private set; }

        /// <summary>
        ///引用用于注册的Ioc容器
        /// </summary>
        public IIocManager IocManager { get; private set; }
    }
}