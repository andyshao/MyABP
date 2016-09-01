using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Entities.Auditing
{
    public abstract class AuditedEntity : AuditedEntity<int>
    {
    }

    public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IModificationAudited
    {
        public virtual DateTime? LastModificationTime
        {
            get;
            set;
        }

        public virtual long? LastModifierUserId
        {
            get;
            set;
        }
    }

    public abstract class AuditedEntity<TPrimaryKey,TUser>:AuditedEntity<TPrimaryKey>,IModificationAudited<TUser>
        where TUser:IEntity<long>
    {

        public virtual TUser ModificationUser
        {
            get;
            set;
        }
    }
}
