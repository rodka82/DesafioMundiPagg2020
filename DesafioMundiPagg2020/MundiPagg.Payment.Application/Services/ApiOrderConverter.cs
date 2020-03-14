using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MundiPagg.Payment.Application.Services
{
    public class ApiOrderConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return objectType.GetTypeInfo().IsClass;
        }
    }
}
