using System;
using System.Runtime.Serialization;

namespace MyABP
{
    /// <summary>
    /// ABP 初始化过程中的异常
    /// </summary>
    [Serializable]
    public class AbpInitializationException : AbpException
    {
        public AbpInitializationException()
        {
        }

        public AbpInitializationException(string message) : base(message)
        {
        }

        public AbpInitializationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected AbpInitializationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}