using System;
using AcklenAvenue.Commands;
using Douglas.Domain.Application.Commands;
using Douglas.Domain.DomainEvents;
using Douglas.Domain.Entities;
using Douglas.Domain.Services;


namespace Douglas.Domain.Application.CommandHandlers
{
    public  class DisablingUser : ICommandHandler<DisableUser>
    {
        public IWriteableRepository writeableRepository { get; private set; }
        public IReadOnlyRepository readOnlyRepository { get; private set; }

        public DisablingUser(IWriteableRepository writeableRepository, IReadOnlyRepository readOnlyRepository)
        {
            this.writeableRepository = writeableRepository;
            this.readOnlyRepository = readOnlyRepository;
        }

        public void Handle(IUserSession userIssuingCommand, DisableUser command)
        {
            var user = readOnlyRepository.GetById<User>(command.id);
            user.DisableUser();

            writeableRepository.Update(user);

            NotifyObservers(new UserDisabled(user.Id));


        }

        public event DomainEvent NotifyObservers;
    }
}