using System.Linq;

using AcklenAvenue.Commands;

using Douglas.Domain.Application.Commands;
using Douglas.Domain.DomainEvents;
using Douglas.Domain.Entities;
using Douglas.Domain.Services;

namespace Douglas.Domain.Application.CommandHandlers
{
    public class UserEmailCreator : ICommandHandler<CreateEmailLoginUser>
    {
        readonly IReadOnlyRepository _readOnlyRepository;

        readonly IWriteableRepository _writeableRepository;

        public UserEmailCreator(IWriteableRepository writeableRepository, IReadOnlyRepository readOnlyRepository)
        {
            _writeableRepository = writeableRepository;
            _readOnlyRepository = readOnlyRepository;
        }

        #region ICommandHandler Members

        public void Handle(IUserSession userIssuingCommand, CreateEmailLoginUser command)
        {
            var userCreated = new UserEmailLogin(
                command.Name, command.Email, command.EncryptedPassword, command.PhoneNumber);

            command.abilities.ToList()
                   .ForEach(x => userCreated.AddAbility(_readOnlyRepository.GetById<UserAbility>(x.Id)));

            UserEmailLogin userSaved = _writeableRepository.Create(userCreated);

            NotifyObservers(new UserEmailCreated(userSaved.Id, command.Email, command.Name, command.PhoneNumber));
        }

        public event DomainEvent NotifyObservers;

        #endregion
    }
}