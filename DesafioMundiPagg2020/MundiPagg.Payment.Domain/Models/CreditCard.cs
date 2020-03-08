using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Domain.Models
{
    public class CreditCard
    {
        public bool Recurrence => false;
        public int Installments { get; set; }
        public string StatementDescriptor { get; set; }
        public Card Card { get; set; }
    }
}
