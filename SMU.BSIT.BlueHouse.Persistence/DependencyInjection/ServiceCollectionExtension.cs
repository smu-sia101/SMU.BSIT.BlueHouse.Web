using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMU.BSIT.BlueHouse.Persistence.OrdersCart;
using SMU.BSIT.BlueHouse.Persistence.Products;

namespace SMU.BSIT.BlueHouse.Persistence.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBlueHousePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConfig = configuration.GetSection("Services:MongoDb");
            var databaseName = GetRequiredConfig(mongoConfig, "Database");
            var connectionString = GetRequiredConfig(mongoConfig, "ConnectionString");

            services.AddSingleton<IProductRepository>(
                new ProductRepository(
                    GetRequiredConfig(mongoConfig, "Collections:Products"),
                    databaseName,
                    connectionString
                ));

            services.AddSingleton<IOrdersCartRepository>(
                new OrdersCartRepository(
                    GetRequiredConfig(mongoConfig, "Collections:OrdersCart"),
                    databaseName,
                    connectionString
                ));

            // TODO: Add other repositories

            return services;
        }

        private static string GetRequiredConfig(IConfiguration config, string key) =>
            config[key] ?? throw new ArgumentException($"Missing configuration: {key}");
    }
}
