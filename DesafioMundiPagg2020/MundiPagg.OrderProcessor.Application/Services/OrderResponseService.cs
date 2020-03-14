using MundiPagg.Domain.Core.Bus;
using MundiPagg.OrderProcessor.Application.Interfaces;
using MundiPagg.OrderProcessor.Domain.Interfaces;
using MundiPagg.OrderProcessor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.OrderProcessor.Application.Services
{
    public class OrderResponseService : IOrderResponseService
    {
        private readonly IOrderResponseRepository _orderResponseRepository;

        private readonly IEventBus _bus;

        public OrderResponseService(IOrderResponseRepository orderRequestRepository, IEventBus bus)
        {
            _orderResponseRepository = orderRequestRepository;
            _bus = bus;
        }

        public IEnumerable<OrderResponse> GetOrderRequests()
        {
            return _orderResponseRepository.GetOrderResponses();
        }
    }
}
