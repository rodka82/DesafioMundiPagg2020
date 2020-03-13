using MundiPagg.Payment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Domain.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<OrderRequest> GetOrders();
        void AddOrder(OrderRequest order);
    }
}
