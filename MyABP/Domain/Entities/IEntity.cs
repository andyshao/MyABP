using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities
{
    /// <summary>
    /// 定义实体类型的接口，系统中所有的实体都必须实现此接口，默认实体主键类型为int
    /// </summary>
    public interface IEntity : IEntity<int>
    {
    }

    /// <summary>
    /// 定义实体类型的接口，系统中所有的实体都必须实现此接口
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体主键类型</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 实体的唯一标识
        /// </summary>
        TPrimaryKey Id { get; set; }
        /// <summary>
        /// 检查实体是否为transient（就是是否会存储到数据库）
        /// </summary>
        /// <returns></returns>
        bool IsTransient();
    }
}
