using Credutpay.Domain.Core.Enums;
using Credutpay.Domain.Core.Event;

namespace Credutpay.Domain.Core.Notification
{
    public class DomainNotification : BaseEvent
    {
        public Guid DomainNotificationId { get; private set; }
        public DomainNotificationType Key { get; private set; }
        public object Value { get; private set; }

        protected DomainNotification(DomainNotificationType key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
        }

        protected DomainNotification(DomainNotificationType key, object value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
        }

        public static DomainNotification Create(DomainNotificationType key, string value)
            => new DomainNotification(key, value);

        public static DomainNotification Create(DomainNotificationType key, object value)
            => new DomainNotification(key, value);
    }
}
