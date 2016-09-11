using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyABP.Modules
{
    /// <summary>
    /// ���ڴ洢ģ����Ϣ
    /// </summary>
    public class AbpModuleInfo
    {
        /// <summary>
        /// ģ������
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// ���ڳ���
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// ����ģ��
        /// </summary>
        public List<AbpModuleInfo> Dependencies { get; }

        /// <summary>
        /// ģ��ʵ��
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