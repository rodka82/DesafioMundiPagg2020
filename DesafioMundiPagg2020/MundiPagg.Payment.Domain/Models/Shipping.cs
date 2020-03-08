using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Domain.Models
{
    public class Shipping
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public Address Address { get; set; }
    }
}