using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Domain.Commands
{
    public class CreateOrderRequestCommand : OrderRequestCommand
    {
        public CreateOrderRequestCommand(int id, DateTime OrderRequestDate)
        {
            Id = id;
            OrderRequestDateTime = OrderRequestDate;
        }
    }
}