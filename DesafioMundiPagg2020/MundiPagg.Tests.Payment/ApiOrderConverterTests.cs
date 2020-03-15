using AutoMapper;
using MundiPagg.Payment.Application.Extensions;
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
        [Fact]
        public void ShouldConvertJsonToSpecifiedFormat()
        {
            string jsonFormatBanco = "{\"numero_pedido\":\"OR1234\",\"comprador\":{\"aniversario\":\"1991-05-20T00:00:00\",\"documento\":\"12345678900\",\"email\":\"fulano@gmail.com\",\"telefone\":\"552122225555\",\"celular\":\"5521999995555\",\"nome\":\"Fulano Silva\",\"tipo\":\"pessoa_fisica\",\"endereco\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"}},\"pagamento\":{\"valor\":210,\"parcelas\":\"5\",\"endereco_cobranca\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"cartao\":{\"bandeira\":\"Visa\",\"numero_cartao\":\"4000000000000010\",\"mes_vencimento\":\"10\",\"ano_vencimento\":\"2022\",\"nome_cartao\":\"FULANO SILVA\",\"cvv\":\"321\"}},\"entrega\":{\"endereco_entrega\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"frete\":10,\"shipping_company\":\"Correios\"},\"carriinho\":{\"items\":[{\"produto\":\"Discos Vinil\",\"quantidade\":10,\"valor_unit\":10},{\"produto\":\"Toca Viniil\",\"quantidade\":1,\"valor_unit\":100}]}}";
            string jsonFormatRequisicao = "{\"numero_pedido\":\"OR1235\",\"comprador\":{\"aniversario\":\"1991-05-20T00:00:00\",\"documento\":\"12345678900\",\"email\":\"fulano@gmail.com\",\"telefone\":\"552122225555\",\"celular\":\"5521999995555\",\"nome\":\"Fulano Silva\",\"tipo\":\"pessoa_fisica\",\"endereco\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"}},\"pagamento\":{\"valor\":210,\"parcelas\":\"5\",\"endereco_cobranca\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"cartao\":{\"bandeira\":\"Visa\",\"numero_cartao\":\"4000000000000010\",\"mes_vencimento\":\"10\",\"ano_vencimento\":\"2022\",\"nome_cartao\":\"FULANO SILVA\",\"cvv\":\"321\"}},\"entrega\":{\"endereco_entrega\":{\"cidade\":\"Rio de Janeiro\",\"complemento\":\"Apto 1011\",\"pais\":\"Brazil\",\"bairro\":\"Centro\",\"numero\":\"123\",\"estado\":\"RJ\",\"logradouro\":\"Rua Souza\",\"cep\":\"23021130\"},\"frete\":10,\"shipping_company\":\"Correios\"},\"carriinho\":{\"items\":[{\"produto\":\"Discos Vinil\",\"quantidade\":10,\"valor_unit\":10},{\"produto\":\"Toca Viniil\",\"quantidade\":1,\"valor_unit\":100}]}}";

            var path1 = GetAllPaths(jsonFormatBanco).OrderBy(x => x).ToList();
            var path2 = GetAllPaths(jsonFormatRequisicao).OrderBy(x => x).ToList();
            
            //O json da requisição tem as mesmas chaves do json do banco
            Assert.True(path1.SequenceEqual(path2));

            //Preenche Dicionários salvos ====================================================================
            Dictionary<string, string> customerPropertyMappings = new Dictionary<string, string>
            {
                {"comprador.email","Email"},
                {"comprador.nome","Name"},
            };

            var dadosJsonRequisicaoDictionary = GetKeyAndValuesToDictionary(jsonFormatRequisicao);

            //o json da requisição pode ser mapeado para MundiPaggOrder.Customer
            var isPossibleGetCustomer = true;
            foreach (var item in customerPropertyMappings)
            {
                var key = item.Key;
                var value = item.Value;
                isPossibleGetCustomer = dadosJsonRequisicaoDictionary
                    .Any(k => RemoveBetween(k.Key, '[', ']') == key);
                if (!isPossibleGetCustomer)
                    break;
            }

            Assert.True(isPossibleGetCustomer);

            //o json da requisição pode ser mapeado para MundiPaggOrder.Itens
            var isPossibleGetItens = true;
            Dictionary<string, string> itemPropertyMappings = new Dictionary<string, string>
            {
                {"carriinho.items.produto", "Description"},
                {"carriinho.items.quantidade", "Quantity"},
                {"carriinho.items.valor_unit", "Amount"}
            };

            foreach (var item in itemPropertyMappings)
            {
                var key = item.Key;
                var value = item.Value;
                isPossibleGetItens = dadosJsonRequisicaoDictionary
                    .Any(k => RemoveBetween(k.Key, '[', ']') == key);

                if (!isPossibleGetItens)
                    break;
            }

            Assert.True(isPossibleGetItens);

            //o json da requisição pode ser mapeado para MundiPaggOrder.Payments
            var isPossibleGetPayments = true;
            Dictionary<string, string> paymentPropertyMappings = new Dictionary<string, string>
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

            foreach (var item in paymentPropertyMappings)
            {
                var key = item.Key;
                var value = item.Value;
                isPossibleGetPayments = dadosJsonRequisicaoDictionary
                    .Any(k => RemoveBetween(k.Key, '[', ']') == key);

                if (!isPossibleGetPayments)
                    break;
            }

            Assert.True(isPossibleGetPayments);

            //Instancia MundiPaggOrder para preencher
            var mundiPaggOrder = new MundiPaggOrder
            {
                Customer = new Customer(),
                Itens = new List<Item>(),
                Payments = new List<MundiPagg.Payment.Application.Models.Payment>()
            };

            foreach (var item in dadosJsonRequisicaoDictionary)
            {
                var key = item.Key;
                var value = item.Value;

                //Customer
                if (!customerPropertyMappings.TryGetValue(key, out var customerProperty))
                    customerProperty = key;
                    
                if (customerProperty == "Name")
                    mundiPaggOrder.Customer.Name = value;

                if (customerProperty == "Email")
                    mundiPaggOrder.Customer.Email = value;
            }

            var itensRequest = dadosJsonRequisicaoDictionary
                .Where(i => itemPropertyMappings.Any(k => RemoveBetween(i.Key, '[', ']') == k.Key))
                .GroupBy(o => o.Key.Substring(0, o.Key.IndexOf(']')))
                .ToDictionary(g => g.Key, g => g.ToList())
                .Select(r => new Item{ 
                    Description= r.Value[0].Value,
                    Quantity = Convert.ToInt32(r.Value[1].Value),
                    Amount = Convert.ToDouble(r.Value[2].Value)
                });

            foreach (var item in itensRequest)
                mundiPaggOrder.Itens.Add(item);

            var paymentsRequest = dadosJsonRequisicaoDictionary
                .Where(i => paymentPropertyMappings.Any(k => RemoveBetween(i.Key, '[', ']') == k.Key))
                .GroupBy(o => o.Key.IndexOf("."))
                .ToDictionary(g => g.Key, g => g.ToList())
                .Select(r => new MundiPagg.Payment.Application.Models.Payment
                {
                    CreditCard = new CreditCard
                    {
                        Card = new Card
                        {
                            BillingAddress = new BillingAddress
                            {
                                City = "",
                                Country = "",
                                Line1 = "",
                                State = "",
                                ZipCode = ""
                            },
                            Cvv = "",
                            ExpMonth = 1,
                            ExpYear = 1,
                            HolderName = "",
                            Number = ""
                        },
                        Installments = 1,
                        StatementDescriptor = "TESTE"
                    }
                }); ;

            foreach (var item in itensRequest)
                mundiPaggOrder.Itens.Add(item);

        }

        private string RemoveBetween(string s, char begin, char end)
        {
            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return regex.Replace(s, string.Empty);
        }

        private IEnumerable<string> GetAllPaths(string json)
        {
            var regex = new Regex(@"\[\d*\].", RegexOptions.Compiled);
            return JObject.Parse(json).DescendantsAndSelf()
                           .OfType<JProperty>()
                           .Where(jp => jp.Value is JValue)
                           .Select(jp => regex.Replace(jp.Path, ".")).Distinct();
        }

        private Dictionary<string,string> GetKeyAndValuesToDictionary(string json)
        {
            var regex = new Regex(@"\[\d*\].", RegexOptions.Compiled);
            return JObject.Parse(json).DescendantsAndSelf()
                           .OfType<JProperty>()
                           .Where(jp => jp.Value is JValue)
                           .Select(jp => new KeyValuePair<string, string>(jp.Path,(string)jp.Value)
                           ).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
