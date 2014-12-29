using System;
using System.Collections.Generic;
using Douglas.Web.Api.Modules;

namespace Douglas.Web.Api.Requests
{
    public class UserAbilitiesRequest
    {
        public IEnumerable<UserAbilityRequest> Abilities { get; set; }
        public Guid UserId { get; set; }
    }
}