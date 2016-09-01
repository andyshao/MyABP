using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities.Auditing
{
    /// <summary>
    /// 为需要创建时间字段的实体，定义此接口
    /// </summary>
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}
