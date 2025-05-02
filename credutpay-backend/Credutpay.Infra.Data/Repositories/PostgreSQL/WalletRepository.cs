using Credutpay.Domain.Registrations.Entities;
using Credutpay.Domain.Registrations.Repositories;
using Credutpay.Domain.Registrations.Repositories.Read;
using Credutpay.Domain.Registrations.ViewModels;
using Credutpay.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Credutpay.Infra.Data.Repositories.PostgreSQL
{
    public class WalletRepository : IWalletRepository, IWalletReadRepository
    {
        private ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddOrUpdate(Wallet wallet)
        {
            try
            {
                var existingEntity = _context.Wallet.FirstOrDefault(x => x.Id == wallet.Id);

                _ = existingEntity == null ? _context.Wallet.Add(wallet) : _context.Wallet.Update(wallet);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding or updating wallet");
            }
        }

        public Wallet GetById(string id)
        {
            try
            {
                return _context.Wallet.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching for the wallet with id {id}.", ex);
            }
        }

        public WalletViewModel GetByIdViewModel(string id)
        {
            try
            {
                var wallet = _context.Wallet
                    .Include(w => w.User)
                    .FirstOrDefault(e => e.Id == id && !e.IsDeleted);

                if (wallet == null)
                    throw new Exception($"Wallet with id {id} not found.");

                if (wallet.User == null)
                    throw new Exception($"User associated with wallet {id} not found.");

                var walletViewModel = new WalletViewModel(
                    wallet.Id,
                    wallet.Amount,
                    new UserViewModel(wallet.User.Id, wallet.User.Name, wallet.User.Email)
                );

                return walletViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching for the wallet with id {id}.", ex);
            }
        }

        public List<WalletTransactionViewModel> GetTransfer(string id, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var wallet = _context.Wallet
                    .Include(w => w.User)
                    .Include(w => w.SentTransactions)
                        .ThenInclude(t => t.WalletReceiver)
                            .ThenInclude(w => w.User)
                    .Include(w => w.ReceiverTransactions)
                        .ThenInclude(t => t.WalletSender)
                            .ThenInclude(w => w.User)
                    .FirstOrDefault(e => e.Id == id && !e.IsDeleted);

                if (wallet == null)
                    return new List<WalletTransactionViewModel>();

                var currentWalletViewModel = new WalletViewModel(
                    wallet.Id,
                    wallet.Amount,
                    new UserViewModel(wallet.User.Id, wallet.User.Name, wallet.User.Email)
                );

                var transactions = new List<WalletTransactionViewModel>();

                var sentTransactions = wallet.SentTransactions.AsEnumerable();
                if (startDate != null && endDate != null)
                {
                    var adjustedEndDate = endDate.Value.Date.AddDays(1).AddTicks(-1);
                    sentTransactions = sentTransactions
                        .Where(t => t.CreatedAt >= startDate && t.CreatedAt <= adjustedEndDate);
                }

                foreach (var transaction in sentTransactions)
                {
                    if (transaction.WalletReceiver?.User == null)
                        continue;

                    var receiverViewModel = new WalletViewModel(
                        transaction.WalletReceiver.Id,
                        transaction.WalletReceiver.Amount,
                        new UserViewModel(
                            transaction.WalletReceiver.User.Id,
                            transaction.WalletReceiver.User.Name,
                            transaction.WalletReceiver.User.Email
                        )
                    );

                    transactions.Add(new WalletTransactionViewModel(
                        transaction.Id,
                        transaction.Amount,
                        receiverViewModel,
                        currentWalletViewModel
                    ));
                }

                var receiverTransactions = wallet.ReceiverTransactions.AsEnumerable();
                if (startDate != null && endDate != null)
                {
                    var adjustedEndDate = endDate.Value.Date.AddDays(1).AddTicks(-1);
                    receiverTransactions = receiverTransactions
                        .Where(t => t.CreatedAt >= startDate && t.CreatedAt <= adjustedEndDate);
                }

                foreach (var transaction in receiverTransactions)
                {
                    if (transaction.WalletSender?.User == null)
                        continue;

                    var senderViewModel = new WalletViewModel(
                        transaction.WalletSender.Id,
                        transaction.WalletSender.Amount,
                        new UserViewModel(
                            transaction.WalletSender.User.Id,
                            transaction.WalletSender.User.Name,
                            transaction.WalletSender.User.Email
                        )
                    );

                    transactions.Add(new WalletTransactionViewModel(
                        transaction.Id,
                        transaction.Amount,
                        currentWalletViewModel,
                        senderViewModel
                    ));
                }

                var allTransactions = new List<(string Id, DateTime CreatedAt)>();

                foreach (var tx in sentTransactions)
                    allTransactions.Add((tx.Id, tx.CreatedAt));

                foreach (var tx in receiverTransactions)
                    allTransactions.Add((tx.Id, tx.CreatedAt));

                var orderedTransactionIds = allTransactions
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => t.Id)
                    .ToList();

                return transactions
                    .OrderBy(vm => orderedTransactionIds.IndexOf(vm.Id))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching for transactions for wallet with id {id}.", ex);
            }
        }
    }
}
