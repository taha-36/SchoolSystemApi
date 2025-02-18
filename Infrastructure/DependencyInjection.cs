using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(op =>
            op.UseSqlServer("Server=MSI\\SQLEXPRESS; Database=SchoolDb; TrustServerCertificate=true; Persist Security Info=true; User ID=me;Password=strongpass12;"));
            return services;
        }
    }
}
