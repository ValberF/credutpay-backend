using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credutpay.Domain.Core.Event
{
    public abstract class BaseEvent : INotification
    {
        public DateTime Timestamp { get; private set; }

        protected BaseEvent()
            => Timestamp = DateTime.Now;
    }
}
