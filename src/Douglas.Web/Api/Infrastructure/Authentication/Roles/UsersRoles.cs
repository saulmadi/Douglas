using System.Collections.Generic;

namespace Douglas.Web.Api.Infrastructure.Authentication.Roles
{
    public class UsersRoles
    {
        public string Name { get; set; }
        public IEnumerable<Feature> Features { get; set; }  

    }
}