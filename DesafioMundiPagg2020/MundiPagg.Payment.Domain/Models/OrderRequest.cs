using System;

namespace MundiPagg.Payment.Domain.Models
{
    public class OrderRequest
    {
        public string Id { get; set; }
        public string Request { get; set; }
        public DateTime RequestDate { get; set; }
        public string Response { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}