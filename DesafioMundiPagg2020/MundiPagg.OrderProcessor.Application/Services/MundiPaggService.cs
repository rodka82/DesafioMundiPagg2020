using MundiAPI.PCL;
using MundiAPI.PCL.Models;
using MundiPagg.OrderProcessor.Application.Factories;
using MundiPagg.OrderProcessor.Domain.Interfaces;
using MundiPagg.Payment.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MundiPagg.OrderProcessor.Application.Services
{
    public sealed class MundiPaggService : IMundiPaggService
    {
        private readonly string _basicAuthUserName = "sk_test_jVZ74df6VtWy72bB";
        private readonly string _basicAuthPassword = "";

        public GetOrderResponse CreateMundiPaggOrder(CreateOrderRequest mundiPaggOrder)
        {
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);

            return client.Orders.CreateOrder(mundiPaggOrder);
        }
    }
}