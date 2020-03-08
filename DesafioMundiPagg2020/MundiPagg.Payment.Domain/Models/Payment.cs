using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Domain.Models
{
    public class Payment
    {
        public string PaymentMethod { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
