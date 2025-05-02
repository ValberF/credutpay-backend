using Credutpay.Domain.Core.Entities;
using Credutpay.Domain.Core.Enums;
using Credutpay.Domain.Registrations.Commands.User;

namespace Credutpay.Domain.Registrations.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public Wallet Wallet { get; private set; }
        public string WalletId { get; set; }

        public User() { }

        private User(CreateUserCommand createUserCommand)
        {
            GenerateId();

            Name = createUserCommand.Name;
            Email = createUserCommand.Email;
            Password = BCrypt.Net.BCrypt.HashPassword(createUserCommand.Password);
            Type = UserType.User;
        }

        public void AddWallet(Wallet wallet) {
            Wallet = wallet;
            WalletId = Wallet.Id;
        }

        public void Update(UpdateUserCommand updateUserCommand)
        {
            UpdateTimestamp();

            Name = updateUserCommand.Name;
        }

        public static User Create(CreateUserCommand createUserCommand) => new User(createUserCommand);

    }
}
