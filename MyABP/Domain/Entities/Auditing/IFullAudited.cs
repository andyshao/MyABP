using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities.Auditing
{
    /// <summary>
    /// 定义通用完整审计接口，保存创建、修改、删除信息
    /// </summary>
    public interface IFullAudited : IAudited, IDeletionAudited
    {

    }
    /// <summary>
    /// 定义通用完整审计接口，保存创建、修改、删除信息，并提供用户导航属性
    /// </summary>
    /// <typeparam name="Tuser"></typeparam>
    public interface IFullAudited<TUser> : IFullAudited, IAudited<TUser>, IDeletionAudited<TUser> where TUser : IEntity<long>
    {

    }
}
