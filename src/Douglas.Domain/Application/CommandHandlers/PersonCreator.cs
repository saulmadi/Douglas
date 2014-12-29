using AcklenAvenue.Commands;

using Douglas.Domain.Application.Commands;
using Douglas.Domain.Entities;
using Douglas.Domain.Services;

namespace Douglas.Domain.Application.CommandHandlers
{
    public class PersonCreator:ICommandHandler<CreatePerson>
    {
        readonly IReadOnlyRepository _readOnlyRepository;

        readonly IWriteableRepository _writeableRepository;

        public PersonCreator(IReadOnlyRepository readOnlyRepository, IWriteableRepository writeableRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeableRepository = writeableRepository;
        }

        public void Handle(IUserSession userIssuingCommand, CreatePerson command)
        {
            var person = new Persona(command.Nombres,command.Apellido);

            _writeableRepository.Create(person);

        }

        public event DomainEvent NotifyObservers;
    }
}