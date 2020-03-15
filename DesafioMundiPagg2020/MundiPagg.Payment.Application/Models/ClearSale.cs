using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    [JsonConverter(typeof(ApiOrderConverter))]
    public class ClearSale
    {
        [JsonProperty("custom_sla")]
        public string custom_sla { get; set; }
    }
}
