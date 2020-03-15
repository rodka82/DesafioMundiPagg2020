using AutoMapper;
using MundiPagg.Payment.Application.Extensions;
using MundiPagg.Payment.Application.Factories;
using MundiPagg.Payment.Application.Models;
using MundiPagg.Payment.Application.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace MundiPagg.Tests.Payment
{
    public class ApiOrderConverterTests
    {
        private Dictionary<string,string> _customerPropertyMappings = new Dictionary<string, string>
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
                {"pagamento.endereco_cobranca.estado","State"},
                {"pagamento.endereco_cobranca.logradouro","Line1"},
                {"pagamento.endereco_cobranca.cep","Zipcode"},
                {"pagamento.cartao.numero_cartao","Number"},
                {"pagamento.cartao.mes_vencimento","ExpMonth"},
                {"pagamento.cartao.ano_vencimento","ExpYear"},
                {"pagamento.cartao.nome_cartao","HolderName"},
                {"pagamento.cartao.cvv","Cvv"}
            };

        [Fact]
        public void ShouldConvertJsonToSpecifiedFormat()
        {
            string jsonFromBank = "{\"numero_pedido\":\"OR1234\",\"comprador\":{\"aniversario\":\"1991-05-20T00:00:00\",\"documento\":\"12345678900\",\"email\":\"fulano@gmail.com\",\"telefone\":\"552122225555\",\"celular\":\"5521999995555\",\"nome\":\"Fulano Silva\",\"tipo\":\"pessoa_fisica\",\"endereco\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"}},\"pagamento\":{\"valor\":210,\"parcelas\":\"5\",\"endereco_cobranca\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"cartao\":{\"bandeira\":\"Visa\",\"numero_cartao\":\"4000000000000010\",\"mes_vencimento\":\"10\",\"ano_vencimento\":\"2022\",\"nome_cartao\":\"FULANO SILVA\",\"cvv\":\"321\"}},\"entrega\":{\"endereco_entrega\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"frete\":10,\"shipping_company\":\"Correios\"},\"carriinho\":{\"items\":[{\"produto\":\"Discos Vinil\",\"quantidade\":10,\"valor_unit\":10},{\"produto\":\"Toca Viniil\",\"quantidade\":1,\"valor_unit\":100}]}}";
            string jsonFromRequest = "{\"numero_pedido\":\"OR1235\",\"comprador\":{\"aniversario\":\"1991-05-20T00:00:00\",\"documento\":\"12345678900\",\"email\":\"fulano@gmail.com\",\"telefone\":\"552122225555\",\"celular\":\"5521999995555\",\"nome\":\"Fulano Silva\",\"tipo\":\"pessoa_fisica\",\"endereco\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"}},\"pagamento\":{\"valor\":210,\"parcelas\":\"5\",\"endereco_cobranca\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"cartao\":{\"bandeira\":\"Visa\",\"numero_cartao\":\"4000000000000010\",\"mes_vencimento\":\"10\",\"ano_vencimento\":\"2022\",\"nome_cartao\":\"FULANO SILVA\",\"cvv\":\"321\"}},\"entrega\":{\"endereco_entrega\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"frete\":10,\"shipping_company\":\"Correios\"},\"carriinho\":{\"items\":[{\"produto\":\"Discos Vinil\",\"quantidade\":10,\"valor_unit\":10},{\"produto\":\"Toca Viniil\",\"quantidade\":1,\"valor_unit\":100}]}}";

            Assert.True(jsonFromRequest.HasSameKeys(jsonFromBank));

            var jsonDataDictionary = jsonFromRequest.GetKeyAndValuesToDictionary();

            //o json da requisição pode ser mapeado para MundiPaggOrder.Customer
            Assert.True(jsonDataDictionary.canBeMappedTo(_customerPropertyMappings));

            //o json da requisição pode ser mapeado para MundiPaggOrder.Itens
            Assert.True(jsonDataDictionary.canBeMappedTo(_itemPropertyMappings));

            //o json da requisição pode ser mapeado para MundiPaggOrder.Payments
            Assert.True(jsonDataDictionary.canBeMappedTo(_paymentPropertyMappings));

            //Instancia MundiPaggOrder para preencher
            var mundiPaggOrder = new MundiPaggOrder
            {
                Customer = new Customer(),
                Itens = new List<Item>(),
                Payments = new List<MundiPagg.Payment.Application.Models.Payment>()
            };

            var payment = PaymentFactory.Create(jsonDataDictionary, _paymentPropertyMappings);
            mundiPaggOrder.Payments.Add(payment);

            var customer = CustomerFactory.Create(jsonDataDictionary, _customerPropertyMappings);
            mundiPaggOrder.Customer = customer;

            var itensRequest = jsonDataDictionary
                .Where(i => _itemPropertyMappings.Any(k => i.Key.RemoveBrackets() == k.Key))
                .GroupBy(o => o.Key.Substring(0, o.Key.IndexOf(']')))
                .ToDictionary(g => g.Key, g => g.ToList())
                .Select(r => new Item
                {
                    Description = r.Value[0].Value,
                    Quantity = Convert.ToInt32(r.Value[1].Value),
                    Amount = Convert.ToDouble(r.Value[2].Value)
                });

            foreach (var item in itensRequest)
                mundiPaggOrder.Itens.Add(item);

        }
    }
}
