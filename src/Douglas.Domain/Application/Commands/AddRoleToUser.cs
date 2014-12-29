using System;

namespace Douglas.Domain.Application.Commands
{
    public class AddRoleToUser 
    {
        public Guid UserId { get; protected set; }
        public Guid RolId { get; protected set; }

        public AddRoleToUser(Guid userId, Guid rolId)
        {
            UserId = userId;
            RolId = rolId;
        }
    }
}