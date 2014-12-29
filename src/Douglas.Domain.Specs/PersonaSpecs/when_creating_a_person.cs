using System;

using AcklenAvenue.Commands;
using AcklenAvenue.Testing.Moq.ExpectedObjects;

using Douglas.Domain.Application.CommandHandlers;
using Douglas.Domain.Application.Commands;
using Douglas.Domain.Entities;
using Douglas.Domain.Services;

using FizzWare.NBuilder;

using Machine.Specifications;

using Moq;

using It = Machine.Specifications.It;

namespace Douglas.Domain.Specs.PersonaSpecs
{
    public class when_creating_a_person
    {
        static CreatePerson _commando;

        static IWriteableRepository _writeableRepository;

        static IReadOnlyRepository _readOnlyRepository;

        static PersonCreator _commandHandler;

        static readonly IUserSession _userIssuingCommand = Mock.Of<IUserSession>();

        static Persona _expectedPerson;

        static string _nombres;

        static string _apellido;

        Establish context = () =>
            {
                _nombres = "Nombres";
                _apellido = "Apellido";
                _commando = new CreatePerson(_nombres, _apellido);
                _writeableRepository = Mock.Of<IWriteableRepository>();
                _readOnlyRepository = Mock.Of<IReadOnlyRepository>();
                _commandHandler = new PersonCreator(_readOnlyRepository, _writeableRepository);
                _expectedPerson =
                    Builder<Persona>.CreateNew()
                                    .With(persona => persona.Id, Guid.Empty)
                                    .With(persona => persona.Nombre, _nombres)
                                    .With(persona => persona.Apellido, _apellido)
                                    .With(persona => persona.Telefono, null)
                                    .Build();
            };

        Because of = () => _commandHandler.Handle(_userIssuingCommand, _commando);

        It should_create_the_person_in_the_db =
            () =>
            Mock.Get(_writeableRepository).Verify(repository => repository.Create(WithExpected.Object(_expectedPerson)));
    }
}