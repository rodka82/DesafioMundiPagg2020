using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    [JsonConverter(typeof(ApiOrderConverter))]
    public class Shipping
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("recipientName")]
        public string RecipientName { get; set; }
        [JsonProperty("recipientPhone")]
        public string RecipientPhone { get; set; }
        [JsonProperty("address")]
        public Address Address { get; set; }
    }
}