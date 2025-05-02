namespace Credutpay.Domain.Registrations.ViewModels
{
    public class WalletViewModel
    {
        public string Id { get; }
        public decimal Amount { get; }
        public UserViewModel User { get; }

        public WalletViewModel(string id, decimal amount, UserViewModel user)
        {
            Id = id;
            Amount = amount;
            User = user;
        }
    }
}
