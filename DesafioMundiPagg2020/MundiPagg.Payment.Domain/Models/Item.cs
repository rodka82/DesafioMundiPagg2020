using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Domain.Models
{
    public class Item
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
