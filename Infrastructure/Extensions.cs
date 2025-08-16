using Infrastructure.Background;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositores.Bids;
using Infrastructure.Persistence.Repositores.Lots;
using Infrastructure.Persistence.Repositores.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    /// <summary>
    /// Подключение инфраструктуры
    /// </summary>
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            /// <summary>
            /// Подключение к базе данных
            /// </summary>
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("DB_Connect"))
            );

            /// <summary>
            /// Инициализация репозиториев IRepo: Repo
            /// </summary>
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILotRepository, LotRepository>();
            services.AddScoped<IBidRepository, BidRepository>();


            /// <summary>
            /// Инициализация фоновых сервисов
            /// </summary>
            services.AddHostedService<LotAutoCloser>();
            services.AddHostedService<WithdrawWonBid>();

            return services;
        }
    }
}
