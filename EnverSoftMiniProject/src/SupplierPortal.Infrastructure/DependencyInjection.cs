using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplierPortal.Domain.Interfaces;
using SupplierPortal.Infrastructure.Data;
using SupplierPortal.Infrastructure.Repositories;

namespace SupplierPortal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ISupplierRepository, SupplierRepository>();

        return services;
    }
}
