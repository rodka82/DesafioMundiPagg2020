using MundiPagg.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.OrderProcessor.Domain.Events
{
    public class OrderRequestCreatedEvent : Event
    {
        public int Id { get; private set; }
        public DateTime OrderRequestDateTime { get; private set; }

        public OrderRequestCreatedEvent(int id, DateTime orderRequestDateTime)
        {
            Id = id;
            OrderRequestDateTime = orderRequestDateTime;
        }
    }
}