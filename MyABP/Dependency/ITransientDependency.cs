namespace MyABP.Dependency
{
    /// <summary>
    /// 所有实现这个接口的类将会作为Transient对象自动进行依赖注入到Ioc容器中
    /// Transient对象:（每次使用都会创新新的对象）
    /// </summary>
    public interface ITransientDependency
    {
    }
}