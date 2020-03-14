using System;

namespace MundiPagg.Payment.Domain.Models
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public string Request { get; set; }
        public DateTime RequestDate { get; set; }
        public string Response { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}