using System;

namespace Douglas.Domain.DomainEvents
{
    public class UserDisabled 
    {
        public Guid id { get; protected set; }

        public UserDisabled(Guid id)
        {
            this.id = id;
        }

        protected UserDisabled()
        {
            
        }
    }
}