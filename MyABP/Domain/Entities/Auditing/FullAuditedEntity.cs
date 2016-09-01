using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities.Auditing
{
    public abstract class FullAuditedEntity
    {
    }

    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IDeletionAudited
    {
        public virtual DateTime? DeletionTime
        {
            get;
            set;
        }

        public virtual long? DeleterUserId
        {
            get;
            set;
        }

        public virtual bool IsDeleted
        {
            get;
            set;
        }
    }

    public abstract class FullAuditedEntity<TPrimaryKey, TUser> : FullAuditedEntity<TPrimaryKey>, IDeletionAudited<TUser>
        where TUser : IEntity<long>
    {

        public virtual TUser DeletionUser
        {
            get;
            set;
        }
    }
}
