using MongoDB.Bson.Serialization;
using MundiPagg.OrderProcessor.Domain.Models;

namespace MundiPagg.OrderProcessor.Data.Mapping
{
    public class OrderResponseMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<OrderResponse>(map =>
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