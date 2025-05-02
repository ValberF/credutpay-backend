using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Notification;
using Credutpay.Domain.Registrations.Commands.Wallet;
using Credutpay.Domain.Registrations.Repositories.Read;
using Credutpay.Infra.Core.API.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Credutpay.API.Controllers
{
    [Route("api/wallet")]
    [ApiController]
    public class WalletController : BaseController
    {
        readonly IWalletReadRepository _walletReadRepository;
        readonly IUserReadRepository _userReadRepository;

        public WalletController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler bus,
            IWalletReadRepository walletReadRepository,
            IUserReadRepository userReadRepository)
            : base(notifications, bus)
        {
            _walletReadRepository = walletReadRepository;
            _userReadRepository = userReadRepository;
        }

        [Authorize(Policy = "User")]
        [HttpGet("")]
        public IActionResult GetBalance()
        {
            try
            {
                var result = _walletReadRepository.GetByIdViewModel(WalletId);
                return HasErrorNotifications ? Error() : Success(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Policy = "User")]
        [HttpGet("transfer")]
        public IActionResult GetTransfers([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var result = _walletReadRepository.GetTransfer(WalletId, startDate, endDate);
                return HasErrorNotifications ? Error() : Success(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Policy = "User")]
        [HttpPost("addfunds")]
        public async Task<IActionResult> AddFunds([FromBody] AddFundsCommand command)
        {
            try
            {
                command.WalletId = WalletId;
                await Bus.SendAsync(command);

                if (HasErrorNotifications)
                    return Error();

                var wallet = _walletReadRepository.GetByIdViewModel(command.WalletId);
                return Success(new { Balance = wallet.Amount });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}