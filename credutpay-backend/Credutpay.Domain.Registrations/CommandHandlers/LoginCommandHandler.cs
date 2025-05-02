using Credutpay.Domain.Core.Auth;
using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Command;
using Credutpay.Domain.Core.Enums;
using Credutpay.Domain.Registrations.Commands.Login;
using Credutpay.Domain.Registrations.Repositories;
using MediatR;

namespace Credutpay.Domain.Registrations.CommandHandlers
{
    public class LoginCommandHandler : BaseCommandHandler,
        INotificationHandler<UserLoginCommand>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IMediatorHandler bus, IJwtService jwtService, IUserRepository userRepository) : base(bus)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public Task Handle(UserLoginCommand notification, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByEmail(notification.Email);

            if (NotifyErrorDomainValidation(user == null, "User not found") ||
                NotifyErrorDomainValidation(!VerifyPassword(notification.Password, user.Password), "Incorrect password")) return Task.CompletedTask;

            var token = _jwtService.GenerateUserToken(user.Id, user.Email, user.WalletId);

            NotifyDomainValidation(DomainNotificationType.Success, token);

            return Task.CompletedTask;
        }

        private bool VerifyPassword(string enteredPassword, string hashedPassword) 
            => BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        
    }
}
