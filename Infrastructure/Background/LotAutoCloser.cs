using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static Domain.Lots.Lot;


namespace Infrastructure.Background
{
    public class LotAutoCloser : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScope;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(30);
        private readonly ILogger<LotAutoCloser> _logger;
        public LotAutoCloser(IServiceScopeFactory serviceScope, ILogger<LotAutoCloser> logger)
        {
            _serviceScope = serviceScope;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start [LotAutoCloser]");
            while (!stoppingToken.IsCancellationRequested)
            {

                using var scope = _serviceScope.CreateScope();
                _logger.LogInformation("Created Scoped");
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                _logger.LogInformation($"Add in scope {ctx}");

                var nowDate = DateTime.UtcNow;

                var Lots = await ctx.Lots.Where(k => k.Status == LotStatus.Active && k.EndTime <= nowDate).ToListAsync();

                foreach (var lot in Lots)
                {
                    lot.CloseLot();
                    _logger.LogInformation($"lot close - {lot}");
                }
                await ctx.SaveChangesAsync();
                _logger.LogInformation($"Save Changes");
                await Task.Delay(_period, stoppingToken);
            }

        }
    }
}
