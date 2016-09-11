namespace MyABP.Dependency
{
    /// <summary>
    /// 通过实现该接口以完成基于约定的依赖注入
    /// </summary>
    public interface IConventionalDependencyRegistrar
    {
        /// <summary>
        /// 基于约定，完成给定程序集的类型的依赖注入
        /// </summary>
        /// <param name="context"></param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}