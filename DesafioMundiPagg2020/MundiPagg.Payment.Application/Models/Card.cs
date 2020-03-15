using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    [JsonConverter(typeof(ApiOrderConverter))]
    public class Card
    {
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("holder_name")]
        public string HolderName { get; set; }
        [JsonProperty("exp_month")]
        public int ExpMonth { get; set; }
        [JsonProperty("exp_year")]
        public int ExpYear { get; set; }
        [JsonProperty("cvv")]
        public string Cvv { get; set; }
        [JsonProperty("billing_address")]
        public BillingAddress BillingAddress { get; set; }
    }
}
