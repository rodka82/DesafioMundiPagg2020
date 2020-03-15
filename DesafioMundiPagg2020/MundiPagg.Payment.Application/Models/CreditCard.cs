using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    [JsonConverter(typeof(ApiOrderConverter))]
    public class CreditCard
    {
        [JsonProperty("recurrence")]
        public bool Recurrence => false;
        [JsonProperty("installments")]
        public int Installments { get; set; }
        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }
        [JsonProperty("card")]
        public Card Card { get; set; }
    }
}
