using MundiPagg.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Domain.Events
{
    public class OrderRequestCreatedEvent : Event
    {
        public string Id { get; private set; }
        public DateTime OrderRequestDateTime { get; private set; }

        public OrderRequestCreatedEvent(string id, DateTime orderRequestDateTime)
        {
            Id = id;
            OrderRequestDateTime = orderRequestDateTime;
        }
    }
}