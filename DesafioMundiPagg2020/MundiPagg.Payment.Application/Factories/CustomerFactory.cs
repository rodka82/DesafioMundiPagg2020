using MundiPagg.Payment.Application.Extensions;
using MundiPagg.Payment.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Factories
{
    public static class CustomerFactory
    {
        public static Customer Create(Dictionary<string, string> keyValuesPair, Dictionary<string, string> mapping)
        {
            var customer = new Customer();
            foreach (var item in keyValuesPair)
            {
                var key = item.Key;
                var value = item.Value.Sanitize();

                if (!mapping.TryGetValue(key, out var customerProperty))
                    customerProperty = key;

                if (customerProperty == "Name")
                    customer.Name = value;

                if (customerProperty == "Email")
                    customer.Email = value;
            }

            return customer;
        } 
    }
}
