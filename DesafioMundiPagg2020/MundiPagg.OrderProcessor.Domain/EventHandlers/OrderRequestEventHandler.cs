using MundiPagg.Domain.Core.Bus;
using MundiPagg.OrderProcessor.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.OrderProcessor.Domain.EventHandlers
{
    public class OrderRequestEventHandler : IEventHandler<OrderRequestCreatedEvent>
    {
        public OrderRequestEventHandler()
        {

        }
        public Task Handle(OrderRequestCreatedEvent @event)
        {
            //Enviar requisição para API da MundiPagg
            return Task.CompletedTask;
        }
    }
}
