using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities.Auditing
{
    /// <summary>
    /// 为需要保存删除信息的实体，提供此接口
    /// </summary>
    public interface IDeletionAudited : ISoftDelete
    {
        DateTime? DeletionTime { get; set; }
        long? DeleterUserId { get; set; }
    }

    /// <summary>
    /// 提供删除用户导航属性接口
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IDeletionAudited<TUser> : IDeletionAudited where TUser : IEntity<long>
    {
        TUser DeletionUser { get; set; }
    }
}
