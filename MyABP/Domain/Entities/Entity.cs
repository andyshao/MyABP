using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities
{
    /// <summary>
    /// 默认实体类型，主键类型为Int
    /// </summary>
    public abstract class Entity : Entity<int>, IEntity
    {
    }

    /// <summary>
    /// 实现实体类型接口
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体主键类型</typeparam>
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 实体的唯一标识
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        /// <summary>
        /// 检查实体是否为transient
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey));
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here

            //相同的实例看作相等
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            //如果为Transient对象，看作为不等
            var other = obj as Entity<TPrimaryKey>;
            if (IsTransient() && other.IsTransient())
            {
                return false;
            }

            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
                return false;

            //最后，若Id相等则看作相等
            return Id.Equals(other.Id);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0} {1}]", GetType().Name, Id);
        }

        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// 不相等比较
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }



    }
}
