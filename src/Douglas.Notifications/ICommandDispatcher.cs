using Autofac.Extras.DynamicProxy2;

namespace Douglas.Notifications
{
   
    public interface ICommandDispatcher
    {
        void Dispatch(IUserSession userSession, object command);
    }
}