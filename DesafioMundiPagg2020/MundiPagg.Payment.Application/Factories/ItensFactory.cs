using MundiAPI.PCL.Models;
using MundiPagg.Payment.Application.Extensions;
using MundiPagg.Payment.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MundiPagg.Payment.Application.Factories
{
    public static class ItensFactory
    {
        public static List<CreateOrderItemRequest> CreateList(Dictionary<string, string> keyValuesPair, Dictionary<string, string> mapping)
        {
            var itens = new List<CreateOrderItemRequest>();
            var itensRequest = keyValuesPair
                            .Where(i => mapping.Any(k => i.Key.RemoveBrackets() == k.Key))
                            .GroupBy(o => o.Key.Substring(0, o.Key.IndexOf(']')))
                            .ToDictionary(g => g.Key, g => g.ToList())
                            .Select(r => new CreateOrderItemRequest
                            {
                                Description = r.Value[0].Value,
                                Quantity = Convert.ToInt32(r.Value[1].Value),
                                Amount = Convert.ToInt32(r.Value[2].Value)
                            });

            foreach (var item in itensRequest)
                itens.Add(item);

            return itens;
        }
    }
}
