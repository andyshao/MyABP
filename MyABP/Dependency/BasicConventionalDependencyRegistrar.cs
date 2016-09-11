using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;

namespace MyABP.Dependency
{
    /// <summary>
    /// 这个类用来注册基本的依赖（实现ITransientDependency、ISingletonDependency的类）
    /// </summary>
    public class BasicConventionalDependencyRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            //注入实现ITransientDependency的对象
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                .IncludeNonPublicTypes()
                .BasedOn<ITransientDependency>()
                .WithService.Self()
                .WithService.DefaultInterfaces()
                .LifestyleTransient()
                );

            //注入实现ISingletonDependency的对象
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                .IncludeNonPublicTypes()
                .BasedOn<ISingletonDependency>()
                .WithService.Self()
                .WithService.DefaultInterfaces()
                .LifestyleSingleton()
                );

            //注入实现IInterceptor的对象
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                .IncludeNonPublicTypes()
                .BasedOn<IInterceptor>()
                .WithService.Self()
                .LifestyleTransient()
                );
        }
    }
}