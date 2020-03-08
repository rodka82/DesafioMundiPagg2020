using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Domain.Models
{
    public class Antifraud
    {
        public string Type { get; set; }
        public ClearSale ClearSale { get; set; }
    }
}
