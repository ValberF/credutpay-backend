using Credutpay.Domain.Core.Command;

namespace Credutpay.Domain.Registrations.Commands.Login
{
    public class UserLoginCommand : BaseCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
