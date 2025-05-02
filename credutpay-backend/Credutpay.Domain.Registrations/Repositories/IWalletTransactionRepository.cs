using Credutpay.Domain.Registrations.Entities;

namespace Credutpay.Domain.Registrations.Repositories
{
    public interface IWalletTransactionRepository
    {
        public void AddOrUpdate(WalletTransaction walletTransaction);
    }
}
