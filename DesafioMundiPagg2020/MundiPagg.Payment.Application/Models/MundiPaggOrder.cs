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
            Itens = new List<Item>();
            Customer = new Customer();
            Payments = new List<Payment>();
        }
        public string Code { get; set; }
        public List<Item> Itens { get; set; }
        public Customer Customer { get; set; }
        public string Ip { get; set; }
        public Location Location { get; set; }
        public Shipping Shipping { get; set; }
        public Antifraud Antifraud { get; set; }
        public string  SessionId { get; set; }
        public Device Device { get; set; }
        public List<Payment> Payments { get; set; }

    }
}
