using Microsoft.Extensions.DependencyInjection;
using MundiPagg.Domain.Core.Bus;
using MundiPagg.Infra.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Infra.IoC
{
    public class DepedencyContainer
    {
        static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>();
        }
    }
}
