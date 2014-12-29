using Douglas.Domain.ValueObjects;

namespace Douglas.Domain.Services
{
    public interface IPasswordEncryptor
    {
        EncryptedPassword Encrypt(string clearTextPassword);
    }
}