using MundiAPI.PCL.Models;
using MundiPagg.Payment.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Factories
{
    public static class MundiPaggOrderFactory
    {
        public static MundiPaggOrder Create(
            Dictionary<string, string> jsonDataDictionary, 
            Dictionary<string,string> customerMap, 
            Dictionary<string, string> paymentMap, 
            Dictionary<string, string> itensMap,
            Dictionary<string, string> orderMap)
        {
            var mundiPaggOrder = new MundiPaggOrder();

            mundiPaggOrder.Items = new List<CreateOrderItemRequest>();
            mundiPaggOrder.Customer = new CreateCustomerRequest();
            mundiPaggOrder.Payments = new List<CreatePaymentRequest>();

            mundiPaggOrder.Code = GetOrderCode(jsonDataDictionary, orderMap);

            var payment = PaymentFactory.Create(jsonDataDictionary, paymentMap);
            mundiPaggOrder.Payments.Add(payment);

            var customer = CustomerFactory.Create(jsonDataDictionary, customerMap);
            mundiPaggOrder.Customer = customer;

            var itens = ItensFactory.CreateList(jsonDataDictionary, itensMap);
            mundiPaggOrder.Items = itens;

            return mundiPaggOrder;
        }

        private static string GetOrderCode(Dictionary<string,string> keyValuesPair, Dictionary<string, string> orderMap)
        {
            string result = string.Empty;
            foreach (var item in keyValuesPair)
            {
                var key = item.Key;
                var value = item.Value;

                if (!orderMap.TryGetValue(key, out string code))
                    code = key;

                if (code == "Code")
                {
                    result = value;
                    break;
                }
            }
            return result;
        }
    }
}