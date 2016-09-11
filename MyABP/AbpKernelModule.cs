using System.Globalization;
using MyABP.Dependency;
using MyABP.Modules;

namespace MyABP
{
    /// <summary>
    /// ABP框架下的核心模块
    /// 其他模块没有必要依赖于此模块，因为该模块总是作为第一个模块被加载
    /// </summary>
    public class AbpKernelModule : AbpModule
    {
        public override void PreInitialize()
        {
            #region 添加注册器
            //添加BasicConventionalDependencyRegistrar到注册器列表中
            IocManager.AddConventionalRegistrar(new BasicConventionalDependencyRegistrar());

            #endregion

            #region 给IApplicationService的派生类注入ValidationInterceptor拦截器
            #endregion 

            #region 给使用了Feature的类注入FeatureInterceptor拦截器
            #endregion 

            #region 给使用了Audited的类注入AuditedInterceptor拦截器
            #endregion 

            #region 给派生自IRepository和IApplicationService的类注入UnitOfWorkInterceptor拦截器
            #endregion 

            #region 给IApplicationService的派生类注入AuthenticationInterceptor拦截器
            #endregion

            #region 给AuditingConfiguration加入默认的选择器
            #endregion

            #region 加入Abp核心框架中的本地化资源
            #endregion

            #region 添加Email、Localization和Notification的Setting
            #endregion

            #region 给UnitOfWork选项中加入过滤器（softdelete,MustHaveTenant,MayHaveTennant）
            #endregion

            #region 配置Cache
            #endregion


        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}