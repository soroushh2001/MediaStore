using MediaStore.Application.Contracts.Persistence;
using MediaStore.Persistence.Data;
using MediaStore.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediaStore.Persistence
{
    public static class PersistenceServiceRegistration 
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services
            ,IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("MediaStoreConnection");
            services.AddDbContext<MediaStoreDbContext>(option =>
            option.UseSqlServer(connectionString));

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

            return services;
        }
    }
}
