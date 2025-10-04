using MediaStore.Application.Contracts.Infrastructure;
using MediaStore.Infrastructure.Senders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediaStore.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
