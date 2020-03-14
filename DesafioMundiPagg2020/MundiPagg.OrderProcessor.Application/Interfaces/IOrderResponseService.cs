using MundiPagg.OrderProcessor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.OrderProcessor.Application.Interfaces
{
    public interface IOrderResponseService
    {
        IEnumerable<OrderResponse> GetOrderRequests();
    }
}
