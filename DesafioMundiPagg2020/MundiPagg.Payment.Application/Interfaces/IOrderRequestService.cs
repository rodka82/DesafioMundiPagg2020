using MundiPagg.Payment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Interfaces
{
    public interface IOrderRequestService
    {
        IEnumerable<OrderRequest> GetOrderRequests();
        void CreateOrder(OrderRequest orderRequest);
    }
}
