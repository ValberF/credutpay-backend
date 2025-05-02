using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Notification;
using Credutpay.Domain.Registrations.Commands.User;
using Credutpay.Domain.Registrations.Repositories.Read;
using Credutpay.Infra.Core.API.Controller;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Credutpay.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        readonly IUserReadRepository _userReadRepository;

        public UserController(INotificationHandler<DomainNotification> notifications, IMediatorHandler bus, IUserReadRepository userReadRepository) 
            : base(notifications, bus)
        {
            _userReadRepository = userReadRepository;
        }

        [Authorize(Policy = "User")]
        [HttpGet("")]
        public IActionResult Get()
        {
            var result = _userReadRepository.GetByIdViewModel(UserId);
            return HasErrorNotifications ? Error() : Success(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserCommand command)
        {
            try
            {
                Bus.SendAsync(command);
                return HasErrorNotifications ? Error() : Success();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Policy = "User")]
        [HttpPut]
        public IActionResult Put([FromBody] UpdateUserCommand command)
        {
            try
            {
                Bus.SendAsync(command);
                return HasErrorNotifications ? Error() : Success();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Policy = "User")]
        [HttpDelete("")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                await Bus.SendAsync(new DeleteUserCommand() { Id = UserId });
                return HasErrorNotifications ? Error() : Success();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
