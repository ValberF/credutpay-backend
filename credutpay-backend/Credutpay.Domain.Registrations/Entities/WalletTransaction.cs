using Credutpay.Domain.Core.Entities;
using Credutpay.Domain.Registrations.Commands.WalletTransaction;

namespace Credutpay.Domain.Registrations.Entities
{
    public class WalletTransaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public Wallet WalletReceiver {  get; set; }
        public Wallet WalletSender { get; set; }
        public string WalletReceiverId { get; set; }
        public string WalletSenderId { get; set; }

        public WalletTransaction() { }

        public WalletTransaction(CreateTransactionCommand createTransactionCommand)
        {
            GenerateId();
            Amount = createTransactionCommand.Amount;
            WalletReceiverId = createTransactionCommand.WalletReceiverId;
            WalletSenderId = createTransactionCommand.WalletSenderId;
        }
    }
}
