﻿using MundiPagg.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.OrderProcessor.Domain.Events
{
    public class OrderRequestCreatedEvent : Event
    {
        public string Id { get; private set; }

        public OrderRequestCreatedEvent(string id)
        {
            Id = id;
        }
    }
}