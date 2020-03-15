using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Application.Models
{
    public class CreditCard
    {
        public CreditCard()
        {
            Card = new Card();
        }
        public bool Recurrence => false;
        public int Installments { get; set; }
        public string StatementDescriptor { get; set; }
        public Card Card { get; set; }
    }
}
