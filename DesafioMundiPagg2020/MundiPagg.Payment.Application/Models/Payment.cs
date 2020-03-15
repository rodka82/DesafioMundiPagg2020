using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    public class Payment
    {
        public Payment()
        {
            CreditCard = new CreditCard();
        }
        public string PaymentMethod => "credit_card";
        public CreditCard CreditCard { get; set; }
    }
}
