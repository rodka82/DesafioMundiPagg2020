using MundiAPI.PCL.Models;
using MundiPagg.Domain.Core.Bus;
using MundiPagg.OrderProcessor.Domain.Events;
using MundiPagg.OrderProcessor.Domain.Interfaces;
using MundiPagg.Payment.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MundiPagg.OrderProcessor.Domain.EventHandlers
{
    public class OrderRequestEventHandler : IEventHandler<OrderRequestCreatedEvent>
    {
        private readonly IMundiPaggService _mundiPaggService;
        private readonly IOrderResponseRepository _orderResponseRepository;
        
        public OrderRequestEventHandler(IMundiPaggService mundiPaggService, IOrderResponseRepository orderResponseRepository)
        {
            _mundiPaggService = mundiPaggService;
            _orderResponseRepository = orderResponseRepository;
        }
        public Task Handle(OrderRequestCreatedEvent @event)
        {
            var orderResponse = _orderResponseRepository.GetOrderResponseById(@event.Id);
            //var mundiPaggOrder = JsonConvert.DeserializeObject<CreateOrderRequest>(orderResponse.Request);
            var mundiPaggOrder = JsonSerializer.Deserialize<CreateOrderRequest>(orderResponse.Request);
            var mundiPaggresponse = _mundiPaggService.CreateMundiPaggOrder(mundiPaggOrder);
            orderResponse.Response = JsonSerializer.Serialize(mundiPaggresponse);
            orderResponse.ResponseDate = DateTime.Now;
            _orderResponseRepository.UpdateOrderResponse(orderResponse);

            return Task.CompletedTask;
        }
    }
}
