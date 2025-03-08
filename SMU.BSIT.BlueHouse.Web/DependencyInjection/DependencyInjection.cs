using SMU.BSIT.BlueHouse.Bus.DependencyInjection;
using SMU.BSIT.BlueHouse.Persistence.DependencyInjection;

namespace SMU.BSIT.BlueHouse.Web.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectBlueHouseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBlueHouseServices();
            services.AddBlueHousePersistence(configuration);
            return services;
        }
    }
}
