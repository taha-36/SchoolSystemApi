using Application;
using Infrastructure;

namespace CleanArchitecture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresDI(this IServiceCollection services)
        {
            services.AddAppDI()
                .AddInfraDI();
            return services;
        }
    }
}
