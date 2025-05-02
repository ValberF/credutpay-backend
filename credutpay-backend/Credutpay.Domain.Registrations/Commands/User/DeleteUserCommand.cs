using Credutpay.Domain.Core.Command;

namespace Credutpay.Domain.Registrations.Commands.User
{
    public class DeleteUserCommand : BaseCommand
    {
        public string Id { get; set; }
    }
}
