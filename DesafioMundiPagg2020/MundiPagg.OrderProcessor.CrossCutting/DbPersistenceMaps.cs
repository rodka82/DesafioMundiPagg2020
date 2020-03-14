using MundiPagg.OrderProcessor.Data.Mapping;

namespace MundiPagg.OrderProcessor.CrossCutting
{
    public class DbPersistenceMaps
    {
        public static void Configure()
        {
            OrderResponseMap.Configure();
        }
    }
}