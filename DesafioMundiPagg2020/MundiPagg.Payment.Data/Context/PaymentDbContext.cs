using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MundiPagg.Payment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg.Payment.Data.Context
{
    public class PaymentDbContext : DbContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; }

        public PaymentDbContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoCollection<Order> Notas
        {
            get
            {
                return _database.GetCollection<Order>("Orders");
            }
        }
    }
}
