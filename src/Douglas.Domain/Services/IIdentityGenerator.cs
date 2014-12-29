namespace Douglas.Domain.Services
{
    public interface IIdentityGenerator<out T>
    {
        T Generate();
    }
}