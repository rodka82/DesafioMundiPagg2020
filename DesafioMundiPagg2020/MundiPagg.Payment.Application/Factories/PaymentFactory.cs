using MundiAPI.PCL.Models;
using MundiPagg.Payment.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Factories
{
    public static class PaymentFactory
    {
        private const string PAYMENT_METHOD = "credit_card";

        public static CreatePaymentRequest Create(Dictionary<string, string> keyValuesPair, Dictionary<string, string> mapping)
        {
            var payment = new CreatePaymentRequest();
            payment.CreditCard = new CreateCreditCardPaymentRequest();
            payment.CreditCard.Card = new CreateCardRequest();
            payment.CreditCard.Card.BillingAddress = new CreateAddressRequest();
            payment.PaymentMethod = PAYMENT_METHOD;

            foreach (var item in keyValuesPair)
            {
                var key = item.Key;
                var value = item.Value.Sanitize();

                if (!mapping.TryGetValue(key, out var paymentProperty))
                    paymentProperty = key;

                if (paymentProperty == "Installments")
                    payment.CreditCard.Installments = Convert.ToInt32(value);

                if (paymentProperty == "Country")
                    payment.CreditCard.Card.BillingAddress.Country = value; 

                if (paymentProperty == "City")
                    payment.CreditCard.Card.BillingAddress.City = value;

                if (paymentProperty == "State")
                    payment.CreditCard.Card.BillingAddress.State = value;

                if (paymentProperty == "Line1")
                    payment.CreditCard.Card.BillingAddress.Line1 = value;

                if (paymentProperty == "ZipCode")
                    payment.CreditCard.Card.BillingAddress.ZipCode = value;

                if (paymentProperty == "Number")
                    payment.CreditCard.Card.Number = value;

                if (paymentProperty == "ExpMonth")
                    payment.CreditCard.Card.ExpMonth = Convert.ToInt32(value);

                if (paymentProperty == "ExpYear")
                    payment.CreditCard.Card.ExpYear = Convert.ToInt32(value);

                if (paymentProperty == "HolderName")
                    payment.CreditCard.Card.HolderName = value;

                if (paymentProperty == "Cvv")
                    payment.CreditCard.Card.Cvv = value;
            }

            return payment;
        }
    }
}
