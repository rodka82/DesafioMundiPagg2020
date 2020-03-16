using MundiPagg.Domain.Core.Bus;
using MundiPagg.Payment.Application.Interfaces;
using MundiPagg.Payment.Data.Repository;
using MundiPagg.Payment.Domain.Commands;
using MundiPagg.Payment.Domain.Interfaces;
using MundiPagg.Payment.Domain.Models;
using System;
using System.Collections.Generic;
using MundiPagg.Payment.Application.Factories;
using System.Text;
using MundiPagg.Payment.Application.Extensions;
using MundiPagg.Payment.Application.Models;
using Newtonsoft.Json;

namespace MundiPagg.Payment.Application.Services
{
    public class OrderRequestService : IOrderRequestService
    {
        private readonly IOrderRequestRepository _orderRequestRepository;
        private Dictionary<string, string> _orderPropertyMappings = new Dictionary<string, string>
        {
            {"numero_pedido","Code"}
        };
        private Dictionary<string, string> _customerPropertyMappings = new Dictionary<string, string>
            {
                {"comprador.email","Email"},
                {"comprador.nome","Name"},
            };
        private Dictionary<string, string> _itemPropertyMappings = new Dictionary<string, string>
            {
                {"carriinho.items.produto", "Description"},
                {"carriinho.items.quantidade", "Quantity"},
                {"carriinho.items.valor_unit", "Amount"}
            };
        private Dictionary<string, string> _paymentPropertyMappings = new Dictionary<string, string>
            {
                {"pagamento.parcelas","Installments"},
                {"pagamento.endereco_cobranca.pais","Country"},
                {"pagamento.endereco_cobranca.cidade","City"},
                {"pagamento.endereco_cobranca.estado","State"},
                {"pagamento.endereco_cobranca.logradouro","Line1"},
                {"pagamento.endereco_cobranca.cep","ZipCode"},
                {"pagamento.cartao.numero_cartao","Number"},
                {"pagamento.cartao.mes_vencimento","ExpMonth"},
                {"pagamento.cartao.ano_vencimento","ExpYear"},
                {"pagamento.cartao.nome_cartao","HolderName"},
                {"pagamento.cartao.cvv","Cvv"}
            };

        private readonly IEventBus _bus;

        public OrderRequestService(IOrderRequestRepository orderRequestRepository, IEventBus bus)
        {
            _orderRequestRepository = orderRequestRepository;
            _bus = bus;
        }

        private void AddOrderRequest(OrderRequest orderRequest)
        {
            _orderRequestRepository.AddOrderRequest(orderRequest);
        }

        public void CreateOrder(string orderRequestJson)
        {
            MundiPaggOrder mundiPaggOrder = MundiPaggOrderFactory.Create(orderRequestJson.GetKeyAndValuesToDictionary(), _customerPropertyMappings, _paymentPropertyMappings, _itemPropertyMappings, _orderPropertyMappings);
            var orderRequest = OrderRequestFactory.Create(mundiPaggOrder);

            var createOrderRequestCommand = new CreateOrderRequestCommand(
                orderRequest.Id,
                DateTime.Now);

            _bus.SendCommand(createOrderRequestCommand);
            AddOrderRequest(orderRequest);
        }

        public IEnumerable<OrderRequest> GetOrderRequests()
        {
            return _orderRequestRepository.GetOrderRequests();
        }
    }
}
