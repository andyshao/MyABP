namespace MyABP.Dependency
{
    /// <summary>
    /// ����ע��ϵͳʹ�õ�������������
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// �������ڵ�һ�ν���ʱ��������֮��Ľ�����������ͬ�Ķ���
        /// </summary>
        Singleton,

        /// <summary>
        /// Transient��Ϊÿһ�ν������󴴽�һ���µĶ���
        /// </summary>
        Transient
    }
}