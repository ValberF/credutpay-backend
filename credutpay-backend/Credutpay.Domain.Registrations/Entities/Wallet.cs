using Credutpay.Domain.Core.Entities;
using Credutpay.Domain.Core.Enums;
using Credutpay.Domain.Registrations.Commands.User;
using Credutpay.Domain.Registrations.Commands.Wallet;
using System.Xml.Linq;

namespace Credutpay.Domain.Registrations.Entities
{
    public class Wallet : BaseEntity
    {
        public decimal Amount { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public ICollection<WalletTransaction> SentTransactions { get; set; } = new List<WalletTransaction>();
        public ICollection<WalletTransaction> ReceiverTransactions { get; set; } = new List<WalletTransaction>();

        public Wallet() { }

        public Wallet(User user)
        {
            GenerateId();
            Amount = 0;
            User = user;
            UserId = user.Id;
        }

        public void AddAmount(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero");

            Amount += amount;
        }

        public void DeductAmount(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero");

            if (Amount < amount)
                throw new InvalidOperationException("Insufficient funds");

            Amount -= amount;
        }

        public void AddAmount(decimal amount, WalletTransaction walletTransaction)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero");

            Amount += amount;
            ReceiverTransactions.Add(walletTransaction);
        }

        public void DeductAmount(decimal amount, WalletTransaction walletTransaction)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero");

            if (Amount < amount)
                throw new InvalidOperationException("Insufficient funds");

            Amount -= amount;
            SentTransactions.Add(walletTransaction);
        }

        public void Update(AddFundsCommand command)
        {
            UpdateTimestamp();
            AddAmount(command.Amount);
        }
    }
}