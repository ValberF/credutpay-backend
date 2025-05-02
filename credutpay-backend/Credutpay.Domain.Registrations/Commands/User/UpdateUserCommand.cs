using Credutpay.Domain.Core.Command;
using Credutpay.Domain.Core.Enums;

namespace Credutpay.Domain.Registrations.Commands.User
{
    public class UpdateUserCommand : BaseCommand
    {
        public string Id { get; set; }
        //public string Email { get; set; }
        public string Name { get; set; }
        public UserType Type { get; set; }
    }
}
