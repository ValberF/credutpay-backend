using Credutpay.Domain.Core.Enums;

namespace Credutpay.Domain.Core.Auth

{
    public interface IJwtService
    {
        string GenerateUserToken(string id, string email, string walletId);
    }
}
