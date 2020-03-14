using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MundiPagg.Domain.Core.Bus;
using MundiPagg.Infra.Bus;
using MundiPagg.Payment.Application.Interfaces;
using MundiPagg.Payment.Application.Services;
using MundiPagg.Payment.Data.Context;
using MundiPagg.Payment.Data.Repository;
using MundiPagg.Payment.Domain.CommandHandlers;
using MundiPagg.Payment.Domain.Commands;
using MundiPagg.Payment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Infra.IoC
{
    public class DepedencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>();

            //Domain Payment Commands
            services.AddTransient<IRequestHandler<CreateOrderRequestCommand, bool>, OrderRequestCommandHandler>();

            //Application Services
            services.AddTransient<IOrderRequestService, OrderRequestService>();

            //Data
            services.AddTransient<IOrderRequestRepository, OrderRequestRepository>();
            services.AddTransient<PaymentDbContext>();
        }
    }
}
