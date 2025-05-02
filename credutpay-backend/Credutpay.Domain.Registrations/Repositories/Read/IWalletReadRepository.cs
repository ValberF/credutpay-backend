using Credutpay.Domain.Registrations.Entities;
using Credutpay.Domain.Registrations.ViewModels;

namespace Credutpay.Domain.Registrations.Repositories.Read
{
    public interface IWalletReadRepository
    {
        WalletViewModel GetByIdViewModel(string id);
        List<WalletTransactionViewModel> GetTransfer(string id, DateTime? startDate, DateTime? endDate);
    }
}
