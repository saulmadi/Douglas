using AcklenAvenue.Commands;
using Nancy;
using Douglas.Domain.Entities;
using Douglas.Domain.Services;

using Douglas.Web.Api.Infrastructure.Authentication;
using Douglas.Web.Api.Infrastructure.Exceptions;
using Douglas.Web.Api.Modules;

namespace Douglas.Web.Api.Infrastructure
{
    public static class IdentityExtentions
    {
        public static IUserSession UserSession(this NancyModule module)
        {
            var user = module.Context.CurrentUser as LoggedInUserIdentity;
            if (user == null) throw new NoCurrentUserException();
            return user.UserSession;
        }

        public static UserLoginSession UserLoginSession(this NancyModule module)
        {
            var user = module.Context.CurrentUser as LoggedInUserIdentity;
            if (user == null || user.UserSession.GetType() != typeof(UserLoginSession)) throw new NoCurrentUserException();
            return (UserLoginSession) user.UserSession;
        }
    }
}