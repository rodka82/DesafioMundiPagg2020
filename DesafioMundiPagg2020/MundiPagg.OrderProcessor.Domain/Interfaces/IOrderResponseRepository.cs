using MundiPagg.OrderProcessor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.OrderProcessor.Domain.Interfaces
{
    public interface IOrderResponseRepository
    {
        IEnumerable<OrderResponse> GetOrderResponses();
        void AddOrderRequest(OrderResponse order);
    }
}
