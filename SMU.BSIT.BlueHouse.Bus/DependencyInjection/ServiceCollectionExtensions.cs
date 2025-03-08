using Microsoft.Extensions.DependencyInjection;
using SMU.BSIT.BlueHouse.Bus.Services.Products;
using SMU.BSIT.BlueHouse.Bus.Services.Security;

namespace SMU.BSIT.BlueHouse.Bus.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlueHouseServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserService,UserService>();
            services.AddSingleton<IProductService, ProductService>();
            
            //TODO: Add other services

            return services;
        }    
    }
}
