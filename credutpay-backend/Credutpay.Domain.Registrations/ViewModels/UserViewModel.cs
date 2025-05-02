namespace Credutpay.Domain.Registrations.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; }
        public string Name { get; }
        public string Email { get; set; }

        public UserViewModel(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
