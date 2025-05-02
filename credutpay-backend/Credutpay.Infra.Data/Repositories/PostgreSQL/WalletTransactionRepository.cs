using Credutpay.Domain.Registrations.Entities;
using Credutpay.Domain.Registrations.Repositories;
using Credutpay.Infra.Data.Context;

namespace Credutpay.Infra.Data.Repositories.PostgreSQL
{
    public class WalletTransactionRepository : IWalletTransactionRepository
    {
        private ApplicationDbContext _context;

        public WalletTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddOrUpdate(WalletTransaction walletTransaction)
        {
            try
            {
                var existingEntity = _context.WalletTransaction.FirstOrDefault(x => x.Id == walletTransaction.Id);

                _ = existingEntity == null ? _context.WalletTransaction.Add(walletTransaction) : _context.WalletTransaction.Update(walletTransaction);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding or updating wallet transaction");
            }
        }
    }
}
