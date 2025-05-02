using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Notification;
using Credutpay.Domain.Registrations.Commands.Wallet;
using Credutpay.Domain.Registrations.Commands.WalletTransaction;
using Credutpay.Domain.Registrations.Repositories.Read;
using Credutpay.Infra.Core.API.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Credutpay.API.Controllers
{
    [Route("api/wallet-transaction")]
    [ApiController]
    public class WalletTransactionController : BaseController
    {
        readonly IWalletReadRepository _walletReadRepository;

        public WalletTransactionController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler bus,
            IWalletReadRepository walletReadRepository,
            IUserReadRepository userReadRepository)
            : base(notifications, bus)
        {
            _walletReadRepository = walletReadRepository;
        }

        [Authorize(Policy = "User")]
        [HttpPost("")]
        public async Task<IActionResult> CreateTransactions([FromBody] CreateTransactionCommand command)
        {
            try
            {
                command.WalletSenderId = WalletId;
                await Bus.SendAsync(command);

                if (HasErrorNotifications)
                    return Error();

                return Success();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
