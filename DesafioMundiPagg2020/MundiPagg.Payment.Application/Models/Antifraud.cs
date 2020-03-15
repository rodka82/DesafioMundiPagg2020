using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    public class Antifraud
    {
        public string Type { get; set; }
        public ClearSale ClearSale { get; set; }
    }
}
