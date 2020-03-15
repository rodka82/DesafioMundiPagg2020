using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    [JsonConverter(typeof(ApiOrderConverter))]
    public class Address
    {
        [JsonProperty("line_1")]
        public string Line1 { get; set; }
        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
