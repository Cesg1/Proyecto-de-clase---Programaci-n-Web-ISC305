using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AgendaForge.Application.Interfaces;
using AgendaForge.Infrastructure.Data;
using AgendaForge.Infrastructure.Repositories;

namespace AgendaForge.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) {
        
        // 1. Configuramos el acceso a PostgreSQL
        services.AddDbContext<AgendaForgeDbContext>(options =>
            options.UseNpgsql(connectionString));

        // 2. LA INYECCIÓN CLAVE (DIP):
        services.AddScoped<IAgendaRepository, AgendaRepository>();

        return services;
    }
}