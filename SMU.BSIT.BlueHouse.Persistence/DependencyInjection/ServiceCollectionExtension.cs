using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMU.BSIT.BlueHouse.Persistence.Products;

namespace SMU.BSIT.BlueHouse.Persistence.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBlueHousePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProductRepository>(
                new ProductRepository(
                    collectionName: configuration.GetSection("Services:MongoDb:Collections:Products").Value
                        ?? throw new ArgumentException("Services:MongoDb:Collections is not found in the appsettings!"),
                    databaseName: configuration.GetSection("Services:MongoDb:Database").Value
                        ?? throw new ArgumentException("Services:MongoDb:Database is not found in the appsettings!"),
                    connectionString: configuration.GetSection("Services:MongoDb:ConnectionString").Value
                        ?? throw new ArgumentException("Services:MongoDb:ConnectionString is not found in the appsettings!")
                ));

            //TODO: Add other repo

            return services;
        }
    }
}
