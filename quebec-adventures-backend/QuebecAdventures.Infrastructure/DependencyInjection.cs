using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuebecAdventures.Application.Interfaces;
using QuebecAdventures.Domain.Interfaces;
using QuebecAdventures.Infrastructure.Persistence;
using QuebecAdventures.Infrastructure.Services;

namespace QuebecAdventures.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("QuebecAdventuresDb")));

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IActivityRepository, ActivityRepository>();

            // Services d'infrastructure (fichiers, emails, etc.)
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}
