using MundiPagg.Payment.Data.Context;
using MundiPagg.Payment.Domain.Interfaces;
using MundiPagg.Payment.Domain.Models;
using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace MundiPagg.Payment.Data.Repository
{
    public class OrderRequestRepository : IOrderRequestRepository
    {
        private PaymentDbContext _context;

        public OrderRequestRepository(PaymentDbContext context)
        {
            _context = context;
        }
        public void AddOrderRequest(OrderRequest order)
        {
            _context.OrderRequests.InsertOne(order);
        }

        public IEnumerable<OrderRequest> GetOrderRequests()
        {
            return _context.OrderRequests.Find(m => true).ToList();
        }
    }
}
