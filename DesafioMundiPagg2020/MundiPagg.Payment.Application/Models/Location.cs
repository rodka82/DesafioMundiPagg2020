using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    [JsonConverter(typeof(ApiOrderConverter))]
    public class Location
    {
        [JsonProperty("location")]
        public string latitude { get; set; }
        [JsonProperty("longitude")]
        public string longitude { get; set; }
    }
}
