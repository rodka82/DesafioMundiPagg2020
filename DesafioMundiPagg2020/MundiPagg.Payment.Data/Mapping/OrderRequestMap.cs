using MongoDB.Bson.Serialization;
using MundiPagg.Payment.Domain.Models;

namespace MundiPagg.Payment.Data.Mapping
{
    public class OrderRequestMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<OrderRequest>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Request).SetIsRequired(true);
                map.MapMember(x => x.RequestDate).SetIsRequired(true);
                map.MapMember(x => x.Response).SetIsRequired(true);
                map.MapMember(x => x.ResponseDate).SetIsRequired(true);
            });
        }
    }
}
