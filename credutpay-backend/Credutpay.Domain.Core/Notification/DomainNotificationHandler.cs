using Credutpay.Domain.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credutpay.Domain.Core.Notification
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
            => _notifications = new List<DomainNotification>();

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }
        public virtual List<DomainNotification> GetNotifications()
           => _notifications;

        public virtual bool HasNotifications()
            => _notifications.Any();

        public virtual bool HasErrorNotifications()
            => HasNotifications() && _notifications.Any(x => x.Key == DomainNotificationType.Error);

        public virtual bool HasNotAllowedNotifications()
            => HasNotifications() && _notifications.Any(x => x.Key == DomainNotificationType.NotAllowed);

        public void Dispose()
            => _notifications = new List<DomainNotification>();
    }
}
