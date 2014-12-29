using System.Collections.Generic;
using System.Linq;
using AcklenAvenue.Data.NHibernate.Testing;
using Douglas.Domain;
using Douglas.Domain.Entities;
using Douglas.Domain.Services;
using Douglas.Domain.ValueObjects;
using Machine.Specifications;
using NHibernate;

namespace Douglas.Data.Specs.WriteableRepositorySpecs
{
    public class when_deleting_all_users
    {
        static IWriteableRepository _writeableRepository;
        static ISession _session;        

        Establish context =
            () =>
                {
                    _session = InMemorySession.New(new MappingScheme("Test"));
                    _writeableRepository = new WriteableRepository(_session);

                    var users = new List<UserEmailLogin>
                                    {
                                        new UserEmailLogin("test1", "test1@test.com", new EncryptedPassword("password")),
                                        new UserEmailLogin("test2", "test2@test.com", new EncryptedPassword("password")),
                                        new UserEmailLogin("test3", "test2@test.com", new EncryptedPassword("password"))
                                    };
                    users.ForEach(x => _session.Save(x));
                    _session.Flush();                    
                };

        Because of =
            () => _writeableRepository.DeleteAll<UserEmailLogin>();

        It should_make_the_change_in_the_session =
            () =>
                {
                    _session.Flush();
                    _session.QueryOver<UserEmailLogin>().List().ShouldBeEmpty();
                };
    }
}