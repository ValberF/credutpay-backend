using Credutpay.Domain.Registrations.Entities;
using Credutpay.Domain.Registrations.Repositories;
using Credutpay.Domain.Registrations.Repositories.Read;
using Credutpay.Domain.Registrations.ViewModels;
using Credutpay.Infra.Data.Context;

namespace Credutpay.Infra.Data.Repositories.PostgreSQL
{
    public class UserRepository : IUserRepository, IUserReadRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddOrUpdate(User user)
        {
            try
            {
                var existingEntity = _context.User.FirstOrDefault(x => x.Id == user.Id);

                _ = existingEntity == null ? _context.User.Add(user) : _context.User.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding or updating user");
            }
        }

        public User GetByEmail(string email)
        {
            try 
            {
                return _context.User.FirstOrDefault(u => u.Email == email && !u.IsDeleted );
            } catch (Exception ex)
            {
                throw new Exception($"Error searching for the user with email {email}.", ex);
            }
            throw new NotImplementedException();
        }

        public UserViewModel GetByIdViewModel(string id)
        {
            try
            {
                var user = _context.User.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
                var userViewModel = new UserViewModel(user.Id, user.Name, user.Email);

                return userViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching for the user with id {id}.", ex);
            }
        }

        public User GetById(string id)
        {
            try
            {
                var user = _context.User.FirstOrDefault(e => e.Id == id && !e.IsDeleted);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching for the user with id {id}.", ex);
            }
        }
    }
}
