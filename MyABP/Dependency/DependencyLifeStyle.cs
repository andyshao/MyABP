namespace MyABP.Dependency
{
    /// <summary>
    /// 依赖注入系统使用的生命周期类型
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// 单例，在第一次解析时创建对象，之后的解析将返回相同的对象
        /// </summary>
        Singleton,

        /// <summary>
        /// Transient，为每一次解析请求创建一个新的对象
        /// </summary>
        Transient
    }
}