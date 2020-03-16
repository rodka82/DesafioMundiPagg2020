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
            string jsonFromDb = "{\"numero_pedido\":\"OR1234\",\"comprador\":{\"aniversario\":\"1991-05-20T00:00:00\",\"documento\":\"12345678900\",\"email\":\"fulano@gmail.com\",\"telefone\":\"552122225555\",\"celular\":\"5521999995555\",\"nome\":\"Fulano Silva\",\"tipo\":\"pessoa_fisica\",\"endereco\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"}},\"pagamento\":{\"valor\":210,\"parcelas\":\"5\",\"endereco_cobranca\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"cartao\":{\"bandeira\":\"Visa\",\"numero_cartao\":\"4000000000000010\",\"mes_vencimento\":\"10\",\"ano_vencimento\":\"2022\",\"nome_cartao\":\"FULANO SILVA\",\"cvv\":\"321\"}},\"entrega\":{\"endereco_entrega\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"frete\":10,\"shipping_company\":\"Correios\"},\"carriinho\":{\"items\":[{\"produto\":\"Discos Vinil\",\"quantidade\":10,\"valor_unit\":10},{\"produto\":\"Toca Viniil\",\"quantidade\":1,\"valor_unit\":100}]}}";
            string jsonFromRequest = "{\"numero_pedido\":\"OR1235\",\"comprador\":{\"aniversario\":\"1991-05-20T00:00:00\",\"documento\":\"12345678900\",\"email\":\"fulano@gmail.com\",\"telefone\":\"552122225555\",\"celular\":\"5521999995555\",\"nome\":\"Fulano Silva\",\"tipo\":\"pessoa_fisica\",\"endereco\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"}},\"pagamento\":{\"valor\":210,\"parcelas\":\"5\",\"endereco_cobranca\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"cartao\":{\"bandeira\":\"Visa\",\"numero_cartao\":\"4000000000000010\",\"mes_vencimento\":\"10\",\"ano_vencimento\":\"2022\",\"nome_cartao\":\"FULANO SILVA\",\"cvv\":\"321\"}},\"entrega\":{\"endereco_entrega\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"frete\":10,\"shipping_company\":\"Correios\"},\"carriinho\":{\"items\":[{\"produto\":\"Discos Vinil\",\"quantidade\":10,\"valor_unit\":10},{\"produto\":\"Toca Viniil\",\"quantidade\":1,\"valor_unit\":100}]}}";
            //string jsonFromRequest = "{\"numero_pedido\":\"OR1234\",\"comprador\":{\"aniversario\":\"1991-05-20T00:00:00\",\"documento\":\"12345678900\",\"email\":\"fulano@gmail.com\",\"telefone\":\"552122225555\",\"celular\":\"5521999995555\",\"nome\":\"Fulano Silva\",\"tipo\":\"pessoa_fisica\",\"endereco\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"BR\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"}},\"pagamento\":{\"valor\":210,\"parcelas\":\"5\",\"endereco_cobranca\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"BR\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"cartao\":{\"bandeira\":\"Visa\",\"numero_cartao\":\"4000000000000010\",\"mes_vencimento\":\"10\",\"ano_vencimento\":\"2022\",\"nome_cartao\":\"FULANO SILVA\",\"cvv\":\"321\"}},\"entrega\":{\"endereco_entrega\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"BR\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"frete\":10,\"shipping_company\":\"Correios\"},\"carriniho\":{\"items\":[{\"produto\":\"Discos Vinil\",\"quantidade\":10,\"valor_unit\":10},{\"produto\":\"Toca Viniil\",\"quantidade\":1,\"valor_unit\":100}]}}";

            Assert.True(jsonFromRequest.HasSameKeys(jsonFromDb));

            var jsonDataDictionary = jsonFromRequest.GetKeyAndValuesToDictionary();

            Assert.True(jsonDataDictionary.CanBeMappedTo(_customerPropertyMappings));
            Assert.True(jsonDataDictionary.CanBeMappedTo(_itemPropertyMappings));
            Assert.True(jsonDataDictionary.CanBeMappedTo(_paymentPropertyMappings));

            var mundiPaggOrder = new MundiPaggOrder();

            var payment = PaymentFactory.Create(jsonDataDictionary, _paymentPropertyMappings);
            mundiPaggOrder.Payments.Add(payment);

            var customer = CustomerFactory.Create(jsonDataDictionary, _customerPropertyMappings);
            mundiPaggOrder.Customer = customer;

            var itens = ItensFactory.CreateList(jsonDataDictionary, _itemPropertyMappings);
            mundiPaggOrder.Items = itens;

            Assert.True(mundiPaggOrder.Items.Any() && mundiPaggOrder.Payments.Any() && !string.IsNullOrEmpty(mundiPaggOrder.Customer.Name));
        }
    }
}
