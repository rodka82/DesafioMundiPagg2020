﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MundiPagg.Domain.Core.Bus;
using MundiPagg.Infra.Bus;
using MundiPagg.OrderProcessor.Application.Interfaces;
using MundiPagg.OrderProcessor.Application.Services;
using MundiPagg.OrderProcessor.Data.Context;
using MundiPagg.OrderProcessor.Data.Repository;
using MundiPagg.OrderProcessor.Domain.EventHandlers;
using MundiPagg.OrderProcessor.Domain.Events;
using MundiPagg.OrderProcessor.Domain.Interfaces;
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
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });

            //Subscriptions
            services.AddTransient<OrderRequestEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<OrderRequestCreatedEvent>,OrderRequestEventHandler>();

            //Domain Payment Commands
            services.AddTransient<IRequestHandler<CreateOrderRequestCommand, bool>, OrderRequestCommandHandler>();

            //Application Services
            services.AddTransient<IOrderRequestService, OrderRequestService>();
            services.AddTransient<IOrderResponseService, OrderResponseService>();
            services.AddTransient<IMundiPaggService, MundiPaggService>();

            //Data
            services.AddTransient<IOrderRequestRepository, OrderRequestRepository>();
            services.AddTransient<IOrderResponseRepository, OrderResponseRepository>();
            services.AddTransient<PaymentDbContext>();
            services.AddTransient<OrderProcessorDbContext>();
        }
    }
}
