using Credutpay.Domain.Registrations.Entities;
using Credutpay.Domain.Registrations.ViewModels;

namespace Credutpay.Domain.Registrations.Repositories.Read
{
    public interface IUserReadRepository
    {
        UserViewModel GetByIdViewModel(string id);
    }
}
