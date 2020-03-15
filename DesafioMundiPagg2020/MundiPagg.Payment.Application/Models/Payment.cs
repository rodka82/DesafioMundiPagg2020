using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    [JsonConverter(typeof(JsonToPaymentConverter))]
    public class Payment
    {
        [JsonProperty("payment_method")]
        public string PaymentMethod => "credit_card";
        [JsonProperty("credit_card")]
        public CreditCard CreditCard { get; set; }
    }
}
