using Credutpay.Domain.Core.Command;

namespace Credutpay.Domain.Registrations.Commands.Wallet
{
    public class GetBalanceCommand : BaseCommand
    {
        public string WalletId { get; set; }
    }
}
