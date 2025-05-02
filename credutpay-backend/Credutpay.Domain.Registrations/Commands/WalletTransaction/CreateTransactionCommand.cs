using Credutpay.Domain.Core.Command;
using System.Text.Json.Serialization;

namespace Credutpay.Domain.Registrations.Commands.WalletTransaction
{
    public class CreateTransactionCommand : BaseCommand
    {
        [JsonIgnore]
        public string? WalletSenderId { get; set; }
        public decimal Amount { get; set; }
        public string WalletReceiverId { get; set; }
    }
}
