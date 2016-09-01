using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities
{
    /// <summary>
    /// 用于规范实体删除模式，标记数据库IsDeleted = true 
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
