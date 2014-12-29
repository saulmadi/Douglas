namespace Douglas.Domain
{
    public interface IUserSessionFactory
    {
        UserSession Create(User executor);
    }
}