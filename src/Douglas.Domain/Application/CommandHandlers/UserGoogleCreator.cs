using System;
using AcklenAvenue.Commands;
using Douglas.Domain.Application.Commands;
using Douglas.Domain.DomainEvents;
using Douglas.Domain.Entities;
using Douglas.Domain.Services;


namespace Douglas.Domain.Application.CommandHandlers
{
    public class UserGoogleCreator : ICommandHandler<CreateGoogleLoginUser>
    {
        readonly IWriteableRepository _writeableRepository;

        public UserGoogleCreator(IWriteableRepository writeableRepository)
        {
            _writeableRepository = writeableRepository;
        }

        public void Handle(IUserSession userIssuingCommand, CreateGoogleLoginUser command)
        {
            var userCreated = _writeableRepository.Create(new UserGoogleLogin(command.DisplayName, command.Email, command.Id, command.GivenName, command.FamilyName, command.ImageUrl, command.Url));
            NotifyObservers(new UserGoogleCreated(userCreated.Id, command.Email, command.DisplayName, command.Id));
        }

        public event DomainEvent NotifyObservers;
    }
}