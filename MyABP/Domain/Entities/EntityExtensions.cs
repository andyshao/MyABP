using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities
{
    /// <summary>
    /// 实体的扩展方法
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// 检查实体为Null还是标记为已删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }
    }
}
