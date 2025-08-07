using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositores.Bids;
using Infrastructure.Persistence.Repositores.Lots;
using Infrastructure.Persistence.Repositores.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) //IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Database=Auction;Username=postgres;Password=12345")
            );

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILotRepository, LotRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            return services;
        }
    }
}
