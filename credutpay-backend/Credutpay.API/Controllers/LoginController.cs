using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Notification;
using Credutpay.Domain.Registrations.Commands.Login;
using Credutpay.Infra.Core.API.Controller;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credutpay.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : BaseController
    {
        public LoginController(INotificationHandler<DomainNotification> notifications, IMediatorHandler bus) : base(notifications, bus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginCommand command)
        {
            try
            {
                await Bus.SendAsync(command);
                return HasErrorNotifications ? Error() : Success(new { token = SuccessNotifications.First().Value });
            } catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
