using Credutpay.Domain.Core.Bus;

namespace Credutpay.Domain.Core.Event
{
    public class BaseEventHandler
    {
        protected readonly IMediatorHandler Bus;

        public BaseEventHandler(IMediatorHandler bus)
        {
            Bus = bus;
        }
    }
}
