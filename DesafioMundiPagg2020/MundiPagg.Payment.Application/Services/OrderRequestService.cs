using MundiPagg.Domain.Core.Bus;
using MundiPagg.Payment.Application.Interfaces;
using MundiPagg.Payment.Data.Repository;
using MundiPagg.Payment.Domain.Interfaces;
using MundiPagg.Payment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Services
{
    public class OrderRequestService : IOrderRequestService
    {
        private readonly IOrderRequestRepository _orderRequestRepository;

        private readonly IEventBus _bus;

        public OrderRequestService(IOrderRequestRepository orderRequestRepository)
        {
            _orderRequestRepository = orderRequestRepository;
        }

        public void AddOrderRequest(OrderRequest orderRequest)
        {
            _orderRequestRepository.AddOrderRequest(orderRequest);
        }

        public IEnumerable<OrderRequest> GetOrderRequests()
        {
            return _orderRequestRepository.GetOrderRequests();
        }
    }
}
