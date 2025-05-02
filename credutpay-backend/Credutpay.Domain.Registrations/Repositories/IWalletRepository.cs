using Credutpay.Domain.Registrations.Entities;

namespace Credutpay.Domain.Registrations.Repositories
{
    public interface IWalletRepository
    {
        public void AddOrUpdate(Wallet wallet);
        public Wallet GetById(string id);
    }
}
