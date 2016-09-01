using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities.Auditing
{
    /// <summary>
    /// 为需要保存修改信息的实体，提供此接口
    /// </summary>
    public interface IModificationAudited
    {
       DateTime? LastModificationTime { get; set; }
       long? LastModifierUserId { get; set; }
    }

    /// <summary>
    /// 提供修改者导航属性接口
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IModificationAudited<TUser> : IModificationAudited where TUser : IEntity<long>
    {
        TUser ModificationUser { get; set; }
    }
}
