using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    [JsonConverter(typeof(ApiOrderConverter))]
    public class Antifraud
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("clearsale")]
        public ClearSale ClearSale { get; set; }
    }
}
