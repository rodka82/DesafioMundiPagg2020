using MundiAPI.PCL.Models;
using MundiPagg.Payment.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.OrderProcessor.Domain.Interfaces
{
    public interface IMundiPaggService
    {
        GetOrderResponse CreateMundiPaggOrder(MundiPaggOrder mundiPaggOrder);
    }
}
