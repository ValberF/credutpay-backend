using Credutpay.Domain.Core.Command;
using System.Text.Json.Serialization;

namespace Credutpay.Domain.Registrations.Commands.Wallet
{
    public class AddFundsCommand : BaseCommand
    {
        [JsonIgnore]
        public string? WalletId { get; set; }
        public decimal Amount { get; set; }
    }
}
