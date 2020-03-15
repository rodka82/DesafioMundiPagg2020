using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    public class Card
    {
        public Card()
        {
            BillingAddress = new BillingAddress();
        }
        public string Number { get; set; }
        public string HolderName { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Cvv { get; set; }
        public BillingAddress BillingAddress { get; set; }
    }
}
