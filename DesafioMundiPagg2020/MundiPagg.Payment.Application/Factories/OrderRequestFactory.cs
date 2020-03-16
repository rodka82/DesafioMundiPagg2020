using MundiPagg.Payment.Domain.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Text;
using MundiPagg.Payment.Application.Models;

namespace MundiPagg.Payment.Application.Factories
{
    public static class OrderRequestFactory
    {
        public static OrderRequest Create(MundiPaggOrder mundiPaggOrder)
        {
            var orderRequest = new OrderRequest();
            orderRequest.Id = mundiPaggOrder.Code;
            orderRequest.Request = JsonSerializer.Serialize(mundiPaggOrder);
            orderRequest.ResponseDate = DateTime.Now;

            return orderRequest;
        }
    }
}
