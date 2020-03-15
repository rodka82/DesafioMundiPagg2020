﻿using MundiPagg.Payment.Application.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace MundiPagg.Payment.Application.Services
{
    public class ApiOrderConverter : JsonConverter
    {
        private readonly Dictionary<string, string> _propertyMappings = new Dictionary<string, string>
        {
            {"pagamento", "payments"},
            {"carriinho", "itens1"},
            {"comprador", "customer"},
            {"endereco_cobranca", "billing_address"},
            {"cartao", "credit_card"},
            {"entrega", "shipping"},
            {"nome", "name"},
            {"email", "email"},
            {"valor", "amount"},
            {"parcelas", "installments"},
            {"cidade", "city"},
            {"pais", "country"},
            {"estado", "state"},
            {"cep", "zip_code"},
            {"logradouro", "line_1"},
            {"numero_cartao", "number"},
            {"nome_cartao", "holder_name"},
            {"cvv", "cvv"},
            {"mes_vencimento", "exp_month"},
            {"ano_vencimento", "exp_year"},
            {"endereco_entrega", "address"},
            {"frete", ""},
            {"produto", "description"},
            {"quantidade", "quantity"},
            {"valor_unitario", "amount"},
            {"numero_pedido", "code"}
        };

        public override bool CanWrite => false;

        public override bool CanConvert(Type ObjectType)
        {
            return ObjectType.GetTypeInfo().IsClass;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object instance = Activator.CreateInstance(objectType);
            var props = objectType.GetTypeInfo().DeclaredProperties.ToList();

            JObject jo = JObject.Load(reader);
            foreach (JProperty jp in jo.Properties())
            {
                if (!_propertyMappings.TryGetValue(jp.Name, out var name))
                    name = jp.Name;

                PropertyInfo prop = props.FirstOrDefault(pi =>
                pi.CanWrite && pi.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == name);

                if (prop != null && prop.PropertyType.Name == typeof(List<object>).Name)
                {
                    if(name == "payments")
                    {
                        var list = new List<Models.Payment>();
                        var paymentObject = JsonConvert.DeserializeObject<Models.Payment>(jp.Value.ToString());
                    }
                }
                else if(prop != null)
                {
                    prop?.SetValue(instance, jp.Value.ToObject(prop.PropertyType, serializer));
                }

                
            }

            return instance;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
