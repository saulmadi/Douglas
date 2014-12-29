namespace Douglas.Domain
{
    public class EncryptedPassword
    {
        public string Password { get; set; }

        public EncryptedPassword(string encryptedPassword)
        {
            Password = encryptedPassword;
        }
    }
}