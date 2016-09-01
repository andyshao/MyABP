using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities.Auditing
{
    /// <summary>
    /// 为需要创建用户字段的实体，定义此接口
    /// </summary>
    public interface ICreationAudited : IHasCreationTime
    {
        long? CreatorUserId { get; set; }
    }

    /// <summary>
    /// 提供创建者导航属性接口
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface ICreationAudited<TUser> : ICreationAudited where TUser : IEntity<long>
    {
        TUser CreationUser { get; set; }
    }
}
