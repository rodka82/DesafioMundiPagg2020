using MundiPagg.Domain.Core.Commands;
using System;

namespace MundiPagg.Payment.Domain.Commands
{
    public abstract class OrderRequestCommand : Command
    {
        public int Id { get; protected set; }
        public DateTime OrderRequestDateTime { get; protected set; }
    }
}