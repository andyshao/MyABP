using System.Globalization;
using MyABP.Dependency;
using MyABP.Modules;

namespace MyABP
{
    /// <summary>
    /// ABP����µĺ���ģ��
    /// ����ģ��û�б�Ҫ�����ڴ�ģ�飬��Ϊ��ģ��������Ϊ��һ��ģ�鱻����
    /// </summary>
    public class AbpKernelModule : AbpModule
    {
        public override void PreInitialize()
        {
            #region ���ע����
            //���BasicConventionalDependencyRegistrar��ע�����б���
            IocManager.AddConventionalRegistrar(new BasicConventionalDependencyRegistrar());

            #endregion

            #region ��IApplicationService��������ע��ValidationInterceptor������
            #endregion 

            #region ��ʹ����Feature����ע��FeatureInterceptor������
            #endregion 

            #region ��ʹ����Audited����ע��AuditedInterceptor������
            #endregion 

            #region ��������IRepository��IApplicationService����ע��UnitOfWorkInterceptor������
            #endregion 

            #region ��IApplicationService��������ע��AuthenticationInterceptor������
            #endregion

            #region ��AuditingConfiguration����Ĭ�ϵ�ѡ����
            #endregion

            #region ����Abp���Ŀ���еı��ػ���Դ
            #endregion

            #region ���Email��Localization��Notification��Setting
            #endregion

            #region ��UnitOfWorkѡ���м����������softdelete,MustHaveTenant,MayHaveTennant��
            #endregion

            #region ����Cache
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