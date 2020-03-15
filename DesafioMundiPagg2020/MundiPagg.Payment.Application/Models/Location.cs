using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    public class Location
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
