using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities.Auditing
{
    /// <summary>
    /// 定义通用审计接口，保存创建、修改信息
    /// </summary>
    public interface IAudited : ICreationAudited, IModificationAudited
    {

    }

    /// <summary>
    /// 定义通用审计接口，保存创建、修改信息，并提供用户导航属性
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IAudited<TUser> : IAudited, ICreationAudited<TUser>, IModificationAudited<TUser> where TUser:IEntity<long>
    {

    }
}
