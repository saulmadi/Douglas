using System;
using AcklenAvenue.Data.NHibernate.Testing;
using Douglas.Domain;
using Douglas.Domain.Entities;
using Machine.Specifications;
using NHibernate;
using Douglas.Domain.Exceptions;

namespace Douglas.Data.Specs.ReadOnlyRepositorySpecs
{
    public class when_getting_first_user_by_query_with_no_match
    {
        static ReadOnlyRepository _readOnlyRepository;
        static Exception _exception;

        Establish context =
            () =>
                {
                    ISession session = InMemorySession.New(new MappingScheme("Test"));
                    _readOnlyRepository = new ReadOnlyRepository(session);
                };

        Because of =
            () => _exception = Catch.Exception(() => _readOnlyRepository.First<UserEmailLogin>(x => x.Name == "test2-match"));

        It should_throw_the_expected_exception =
            () => _exception.ShouldBeOfType<ItemNotFoundException<UserEmailLogin>>();
    }
}