using System;

namespace Douglas.Domain.DomainEvents
{
    public class PasswordReset
    {
        public Guid UserId { get; private set; }

        public PasswordReset(Guid userId)
        {
            UserId = userId;
        }
    }
}