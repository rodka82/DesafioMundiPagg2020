using MundiPagg.OrderProcessor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.OrderProcessor.Domain.Interfaces
{
    public interface IOrderResponseRepository
    {
        IEnumerable<OrderResponse> GetOrderResponses();
        OrderResponse GetOrderResponseById(int id);
        void AddOrderResponse(OrderResponse order);
        void UpdateOrderResponse(OrderResponse order);
    }
}