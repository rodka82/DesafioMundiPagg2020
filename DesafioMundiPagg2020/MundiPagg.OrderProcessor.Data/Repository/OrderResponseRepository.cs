using MundiPagg.OrderProcessor.Data.Context;
using MundiPagg.OrderProcessor.Domain.Interfaces;
using MundiPagg.OrderProcessor.Domain.Models;
using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace MundiPagg.OrderProcessor.Data.Repository
{
    public class OrderResponseRepository : IOrderResponseRepository
    {
        private OrderProcessorDbContext _context;

        public OrderResponseRepository(OrderProcessorDbContext context)
        {
            _context = context;
        }
        public void AddOrderRequest(OrderResponse order)
        {
            _context.OrderResponses.InsertOne(order);
        }

        public IEnumerable<OrderResponse> GetOrderResponses()
        {
            return _context.OrderResponses.Find(m => true).ToList();
        }
    }
}
