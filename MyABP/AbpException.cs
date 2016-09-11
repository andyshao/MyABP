using System;
using System.Runtime.Serialization;

namespace MyABP
{
    /// <summary>
    /// 异常基类：用于抛出所有ABP的异常
    /// </summary>
    [Serializable]
    public class AbpException : Exception
    {
        public AbpException()
        {
        }

        public AbpException(string message) : base(message)
        {
        }

        public AbpException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AbpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}