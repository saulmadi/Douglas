﻿using System.Collections.Generic;
using AcklenAvenue.Data.NHibernate.Testing;
using Douglas.Domain;
using Douglas.Domain.Entities;
using Douglas.Domain.ValueObjects;
using Machine.Specifications;
using NHibernate;

namespace Douglas.Data.Specs.ReadOnlyRepositorySpecs
{
    public class when_getting_all_users
    {
        static ReadOnlyRepository _readOnlyRepository;
        static IEnumerable<UserEmailLogin> _result;
        static List<UserEmailLogin> _users;

        Establish context =
            () =>
                {
                    ISession session = InMemorySession.New(new MappingScheme("Test"));

                    _users = new List<UserEmailLogin>
                                 {
                                     new UserEmailLogin("test1", "test1@test.com", new EncryptedPassword("password")),
                                     new UserEmailLogin("test2", "test2@test.com", new EncryptedPassword("password"))
                                 };

                    _users.ForEach(x => session.Save(x));
                    session.Flush();

                    _readOnlyRepository = new ReadOnlyRepository(session);
                };

        Because of =
            () => _result = _readOnlyRepository.GetAll<UserEmailLogin>();

        It should_return_all_the_users =
            () =>
                {
                    _result.ShouldBeLike(_users);
                };
    }
}