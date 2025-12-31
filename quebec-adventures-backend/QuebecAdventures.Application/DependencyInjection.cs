using Microsoft.Extensions.DependencyInjection;
using QuebecAdventures.Application.Interfaces;
using QuebecAdventures.Application.Services;

namespace QuebecAdventures.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IReviewService, ReviewService>();
            
            return services;
        }
    }
}
