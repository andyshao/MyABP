using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities.Auditing
{
    public abstract class CreationAuditedEntity : CreationAuditedEntity<int>
    {
    }

    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
        public CreationAuditedEntity()
        {
            CreationTime = DateTime.Now;
        }
        public virtual long? CreatorUserId
        {
            get;
            set;
        }

        public virtual DateTime CreationTime
        {
            get;
            set;
        }
    }

    public abstract class CreationAuditedEntity<TPrimaryKey, TUser> : CreationAuditedEntity<TPrimaryKey>, ICreationAudited<TUser>
        where TUser : IEntity<long>
    {
        public virtual TUser CreationUser
        {
            get;
            set;
        }
    }
}
