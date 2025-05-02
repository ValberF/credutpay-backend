using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Command;
using Credutpay.Domain.Core.Event;
using MediatR;

namespace Credutpay.Infra.Core.Bus
{
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseAsync<T>(T @event) where T : BaseEvent
            => PublishAsync(@event);

        public Task SendAsync<T>(T command) where T : BaseCommand
            => PublishAsync(command);

        private Task PublishAsync<T>(T message) where T : INotification
            => _mediator.Publish(message);
    }
}
