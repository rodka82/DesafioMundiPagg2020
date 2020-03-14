using MundiPagg.Domain.Core.Bus;
using MundiPagg.Payment.Application.Interfaces;
using MundiPagg.Payment.Data.Repository;
using MundiPagg.Payment.Domain.Commands;
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

        public OrderRequestService(IOrderRequestRepository orderRequestRepository, IEventBus bus)
        {
            _orderRequestRepository = orderRequestRepository;
            _bus = bus;
        }

        public void AddOrderRequest(OrderRequest orderRequest)
        {
            _orderRequestRepository.AddOrderRequest(orderRequest);
        }

        public void CreateOrder(OrderRequest orderRequest)
        {
            var createOrderRequestCommand = new CreateOrderRequestCommand(
                orderRequest.Id,
                orderRequest.RequestDate);

            _bus.SendCommand(createOrderRequestCommand);
        }

        public IEnumerable<OrderRequest> GetOrderRequests()
        {
            return _orderRequestRepository.GetOrderRequests();
        }
    }
}
