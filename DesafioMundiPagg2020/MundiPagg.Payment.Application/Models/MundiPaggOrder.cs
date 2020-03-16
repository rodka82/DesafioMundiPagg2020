using MundiPagg.Payment.Application.Services;
using MundiPagg.Payment.Application.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using MundiAPI.PCL.Models;

namespace MundiPagg.Payment.Application.Models
{
    public class MundiPaggOrder
    {
        public MundiPaggOrder()
        {
            Items = new List<CreateOrderItemRequest>();
            Customer = new CreateCustomerRequest();
            Payments = new List<CreatePaymentRequest>();
        }
        public string Code { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; }
        public CreateCustomerRequest Customer { get; set; }
        public string Ip { get; set; }
        public Location Location { get; set; }
        public Shipping Shipping { get; set; }
        public Antifraud Antifraud { get; set; }
        public string  SessionId { get; set; }
        public Device Device { get; set; }
        public List<CreatePaymentRequest> Payments { get; set; }

    }
}
