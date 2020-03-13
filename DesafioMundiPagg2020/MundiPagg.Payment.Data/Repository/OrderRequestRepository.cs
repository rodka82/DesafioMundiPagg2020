using MundiPagg.Payment.Data.Context;
using MundiPagg.Payment.Domain.Interfaces;
using MundiPagg.Payment.Domain.Models;
using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace MundiPagg.Payment.Data.Repository
{
    public class OrderRequestRepository : IOrderRepository
    {
        private PaymentDbContext _context;

        public OrderRequestRepository(PaymentDbContext context)
        {
            _context = context;
        }
        public void AddOrder(OrderRequest order)
        {
            _context.Orders.InsertOne(order);
        }

        public IEnumerable<OrderRequest> GetOrders()
        {
            return _context.Orders.Find(m => true).ToList();
        }
    }
}
