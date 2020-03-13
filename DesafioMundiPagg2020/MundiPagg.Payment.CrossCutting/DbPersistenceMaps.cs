using MundiPagg.Payment.Data.Mapping;

namespace MundiPagg.Payment.CrossCutting
{
    public class DbPersistenceMaps
    {
        public static void Configure()
        {
            OrderRequestMap.Configure();
        }
    }
}