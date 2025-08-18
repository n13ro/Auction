using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Background
{
    public class CheckRefreshTokenLife : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScope;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(30);

        public CheckRefreshTokenLife(IServiceScopeFactory serviceScope)
        {
            _serviceScope = serviceScope;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScope.CreateScope();
                    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var now = DateTime.UtcNow;

                    var users = await ctx.Users
                        .Where(k => k.RefreshToken != null && k.RefreshTokenLife != null)
                        .ToListAsync(stoppingToken);

                    foreach (var user in users)
                    {
                        if (user.RefreshTokenLife < DateTime.UtcNow)
                        {
                            user.RevorkeRefreshToken();
                            await ctx.SaveChangesAsync();
                        }
                    }

                    await Task.Delay(_period, stoppingToken);
                }
                catch(Exception ex)
                {
                    throw new InvalidOperationException("{}", ex);
                }
               
            }
        }
    }
}