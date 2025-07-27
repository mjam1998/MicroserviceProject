

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Contracts;
using Ordering.Infrastructure.Repositories;
using System.Reflection;

namespace Ordering.Infrastructure
{
    public  static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        string connectionString)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(connectionString));

           

            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
