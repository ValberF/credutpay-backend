using Credutpay.Domain.Core.Command;
using Credutpay.Domain.Core.Enums;

namespace Credutpay.Domain.Registrations.Commands.User
{
    public class CreateUserCommand : BaseCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
