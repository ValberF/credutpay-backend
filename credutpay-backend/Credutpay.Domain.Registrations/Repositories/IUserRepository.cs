using Credutpay.Domain.Registrations.Entities;

namespace Credutpay.Domain.Registrations.Repositories
{
    public interface IUserRepository
    {
        public void AddOrUpdate(User user);
        public User GetByEmail(string email);
        public User GetById(string id);

    }
}
