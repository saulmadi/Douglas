using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Douglas.Domain.Entities;

namespace Douglas.Data
{
    public class UserLoginSessionAutoMappingOverride : IAutoMappingOverride<UserLoginSession>
    {
        public void Override(AutoMapping<UserLoginSession> mapping)
        {            
        }
    }
}