using System;

namespace Douglas.Domain.Entities
{
    public class Role:IEntity
    {
        

        public Role(Guid id, string description)
        {
            Id = id;
            this.Description = description;
        }

        protected Role()
        {
            
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Description { get;protected set; }
    }
}