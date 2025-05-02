using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Command;
using Credutpay.Domain.Registrations.Commands.User;
using Credutpay.Domain.Registrations.Entities;
using Credutpay.Domain.Registrations.Repositories;
using Credutpay.Domain.Registrations.Repositories.Read;
using MediatR;

namespace Credutpay.Domain.Registrations.CommandHandlers
{
    public class UserCommandHandler : BaseCommandHandler,
        INotificationHandler<CreateUserCommand>,
        INotificationHandler<UpdateUserCommand>,
        INotificationHandler<DeleteUserCommand>
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        public UserCommandHandler(
            IMediatorHandler bus,
            IUserReadRepository userReadRepository,
            IUserRepository userRepository,
            IWalletRepository walletRepository) : base(bus)
        {
            _userReadRepository = userReadRepository;
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        public Task Handle(CreateUserCommand notification, CancellationToken cancellationToken)
        {
            var existingUser = _userRepository.GetByEmail(notification.Email);

            if (NotifyErrorDomainValidation(existingUser != null, "Email already registered"))
                return Task.CompletedTask;

            var user = User.Create(notification);
            var wallet = new Wallet(user);

            user.AddWallet(wallet);

            _userRepository.AddOrUpdate(user);
            _walletRepository.AddOrUpdate(wallet);

            return Task.CompletedTask;
        }

        public Task Handle(UpdateUserCommand notification, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(notification.Id);

            if (NotifyErrorDomainValidation(user == null, "User not found"))
                return Task.CompletedTask;

            user.Update(notification);
            _userRepository.AddOrUpdate(user);
            return Task.CompletedTask;
        }

        public Task Handle(DeleteUserCommand notification, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(notification.Id);

            if (NotifyErrorDomainValidation(user == null, "User not found"))
                return Task.CompletedTask;

            user.Disable();
            _userRepository.AddOrUpdate(user);
            return Task.CompletedTask;
        }
    }
}