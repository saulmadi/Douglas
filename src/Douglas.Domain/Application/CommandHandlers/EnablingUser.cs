using AcklenAvenue.Commands;
using Douglas.Domain.Application.Commands;
using Douglas.Domain.DomainEvents;
using Douglas.Domain.Entities;
using Douglas.Domain.Services;


namespace Douglas.Domain.Application.CommandHandlers
{
    public  class EnablingUser : ICommandHandler<EnableUser>
    {
        readonly IWriteableRepository _writeableRepository;
        readonly IReadOnlyRepository _readOnlyRepository;

        public EnablingUser(IWriteableRepository writeableRepository, IReadOnlyRepository readOnlyRepository)
        {
            _writeableRepository = writeableRepository;
            _readOnlyRepository = readOnlyRepository;
        }

        public void Handle(IUserSession userIssuingCommand, EnableUser command)
        {
            var user = _readOnlyRepository.GetById<User>(command.id);

            user.EnableUser();

            _writeableRepository.Update(user);

            NotifyObservers(new UserEnabled(user.Id));
        }

        public event DomainEvent NotifyObservers;
    }
}