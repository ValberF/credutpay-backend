using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ApplicationInsights;
using Credutpay.Domain.Core.Enums;
using System.Net;
using Credutpay.Infra.Core.API.Response;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Credutpay.Infra.Core.API.Controller
{
    public class BaseController : ControllerBase, IDisposable
    {
        protected readonly IMediatorHandler Bus;
        readonly DomainNotificationHandler _notifications;
        readonly TelemetryClient _telemetry = new TelemetryClient();

        public BaseController(INotificationHandler<DomainNotification> notifications, IMediatorHandler bus)
        {
            Bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected bool HasErrorNotifications { get { return _notifications.HasErrorNotifications(); } }

        protected IEnumerable<DomainNotification> SuccessNotifications { get { return _notifications.GetNotifications().Where(x => x.Key == DomainNotificationType.Success); } }

        protected IActionResult Error(string errorMessage)
            => BadRequest(EnvelopeResponse.Error(errorMessage));

        protected IActionResult InternalServerError(Exception exception)
        {
            _telemetry.TrackException(exception);

            return StatusCode((int)HttpStatusCode.InternalServerError, EnvelopeResponse.Error(exception.Message));
        }
        protected IActionResult InternalServerError(Exception exception, string message)
        {
            _telemetry.TrackException(exception);

            return StatusCode((int)HttpStatusCode.InternalServerError, EnvelopeResponse.Error(message));
        }

        protected IActionResult Error()
        {
            var notificationValues = _notifications.GetNotifications().Where(x => x.Key == DomainNotificationType.Error).Select(n => n.Value);

            return BadRequest(EnvelopeResponse.Error(string.Join(", ", notificationValues)));
        }

        protected IActionResult Error(HttpStatusCode statusCode, string errorMessage)
            => StatusCode((int)statusCode, EnvelopeResponse.Error(errorMessage));

        protected IActionResult Success<T>(T value) where T : class
            => Ok(EnvelopeResponse.Success(value));

        protected IActionResult Success<T>(HttpStatusCode statusCode, T value) where T : class
            => StatusCode((int)statusCode, EnvelopeResponse.Success(value));

        protected IActionResult Success()
            => Ok(EnvelopeResponse.Success());

        private IEnumerable<Claim> GetUserClaims()
        {
            var authorizationHeader = Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(authorizationHeader.ToString()))
                return new List<Claim>();

            try
            {
                var token = authorizationHeader.ToString()
                    .Substring("Bearer ".Length)
                    .Trim();

                var jwtSecurity = new JwtSecurityTokenHandler().ReadJwtToken(token);

                return jwtSecurity.Claims;
            }
            catch
            {
                return new List<Claim>();
            }
        }

        protected string UserId
        {
            get
            {
                var claims = GetUserClaims();

                var userIdClaim = claims.FirstOrDefault(x => x.Type == "Id");

                if (userIdClaim is null) return string.Empty;

                return userIdClaim.Value;
            }
        }

        protected string WalletId
        {
            get
            {
                var claims = GetUserClaims();

                var walletIdClaim = claims.FirstOrDefault(x => x.Type == "WalletId");

                if (walletIdClaim is null) return string.Empty;

                return walletIdClaim.Value;
            }
        }

        public void Dispose()
        {
            _notifications.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
