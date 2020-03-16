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
        private readonly string _basicAuthUserName = "sk_XdoMjRfrYta3MWgV";
        private readonly string _basicAuthPassword = "";

        public GetOrderResponse CreateMundiPaggOrder(MundiPaggOrder mundiPaggOrder)
        {
            var client = new MundiAPIClient(_basicAuthUserName, _basicAuthPassword);
            var order = OrderRequestFactory.Create(mundiPaggOrder);

            return client.Orders.CreateOrder(order);
        }
    }
}