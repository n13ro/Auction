using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Lots.Lot;

namespace Infrastructure.Background
{
    public class LotAutoCloser : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScope;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(30);
        private readonly ILogger _logger;
        public LotAutoCloser(IServiceScopeFactory serviceScope, ILogger<LotAutoCloser> logger)
        {
            _serviceScope = serviceScope;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            _logger.LogInformation("Lot closing algorithm has started.");
            Console.ForegroundColor = originalColor;
            //_logger.LogInformation("Lot closing algorithm has started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                
                try
                {
                    var currentTime = DateTime.UtcNow;
                    using var scope = _serviceScope.CreateScope();
                    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var expiredLots = await ctx.Lots
                        .Where(t => t.Status == LotStatus.Active && t.EndTime <= currentTime)
                        .ToListAsync(stoppingToken);

                    if (expiredLots.Any())
                    {
                        foreach (var lot in expiredLots)
                        {
                            lot.CloseLot();
                            _logger.LogInformation($"Closed lot - {lot.Name}, {lot.Description}");
                        }
                        await ctx.SaveChangesAsync(stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    //var originalColor = Console.ForegroundColor;
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //_logger.LogError(ex, "Error in LotAutoCloser");
                    //Console.ForegroundColor = originalColor;

                    
                }

                await Task.Delay(_period, stoppingToken);
            }
            _logger.LogInformation("Succesfully closed all expired lots.");
        }
    }
}