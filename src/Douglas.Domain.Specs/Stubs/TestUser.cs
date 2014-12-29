using System;
using Douglas.Domain.Entities;
using Douglas.Domain.ValueObjects;

namespace Douglas.Domain.Specs.Stubs
{
    public class TestUser : UserEmailLogin
    {
        public TestUser(Guid userId, string name, string password)
        {
            Id = userId;
            Name = name;
            this.EncryptedPassword = password;
        }
    }
}