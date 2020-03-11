using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
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
