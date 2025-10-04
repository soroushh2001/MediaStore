using MediaStore.Application.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediaStore.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(BrandMappingProfile),typeof(CategoryMappingProfile)
                ,typeof(ProductMappingProfile));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
