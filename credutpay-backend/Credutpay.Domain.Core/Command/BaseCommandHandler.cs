using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Enums;
using Credutpay.Domain.Core.Notification;

namespace Credutpay.Domain.Core.Command
{
    public class BaseCommandHandler
    {
        protected readonly IMediatorHandler Bus;

        protected BaseCommandHandler(IMediatorHandler bus)
        {
            Bus = bus;
        }

        protected void NotifyDomainValidation<T>(DomainNotificationType key, T value)
            => Bus.RaiseAsync(DomainNotification.Create(key, value));

        protected bool NotifyErrorDomainValidation<T>(bool condition, T value)
        {
            if (condition) Bus.RaiseAsync(DomainNotification.Create(DomainNotificationType.Error, value));
            return condition;
        }
    }
}
