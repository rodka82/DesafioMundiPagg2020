using MediatR;
using MundiPagg.Domain.Core.Bus;
using MundiPagg.Payment.Domain.Commands;
using MundiPagg.Payment.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MundiPagg.Payment.Domain.CommandHandlers
{
    public class OrderRequestCommandHandler : IRequestHandler<CreateOrderRequestCommand, bool>
    {
        private readonly IEventBus _bus;

        public OrderRequestCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateOrderRequestCommand request, CancellationToken cancellationToken)
        {
            //publicar no RabbitMQ
            _bus.Publish(new OrderRequestCreatedEvent(request.Id, request.OrderRequestDateTime));

            return Task.FromResult(true);
        }
    }
}
