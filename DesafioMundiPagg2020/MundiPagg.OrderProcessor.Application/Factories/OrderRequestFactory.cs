using MundiAPI.PCL.Models;
using MundiPagg.Payment.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MundiPagg.OrderProcessor.Application.Factories
{
    public static class OrderRequestFactory
    {
        public static CreateOrderRequest Create(MundiPaggOrder mundiPaggOrder)
        {
            CreateCustomerRequest customer = mundiPaggOrder.Customer;
            List<CreateOrderItemRequest> items = mundiPaggOrder.Items.Cast<CreateOrderItemRequest>().ToList();
            List<CreatePaymentRequest> payments = mundiPaggOrder.Payments.Cast<CreatePaymentRequest>().ToList();

            var order = new CreateOrderRequest()
            {
                Items = items,
                Customer = customer,
                Payments = payments
            };

            return order;
        }
    }
}
