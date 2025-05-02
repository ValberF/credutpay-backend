using Credutpay.Domain.Registrations.ViewModels;

namespace Credutpay.Domain.Registrations.ViewModels
{
    public class WalletTransactionViewModel
    {
        public string Id { get; }
        public decimal Amount { get; }
        public WalletViewModel WalletReceiver { get; }
        public WalletViewModel WalletSender { get; }

        public WalletTransactionViewModel(string id, decimal amount, WalletViewModel walletReceiver, WalletViewModel walletSender)
        {
            Id = id;
            Amount = amount;
            WalletReceiver = walletReceiver;
            WalletSender = walletSender;
        }
    }
}