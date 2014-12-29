using Douglas.Domain.Entities;

namespace Douglas.Domain.Services
{
    public interface IUserSessionFactory
    {
        UserLoginSession Create(User executor);
    }
}