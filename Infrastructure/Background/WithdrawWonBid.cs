using Domain.Lots;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using static Domain.Bids.Bid;
using static Domain.Lots.Lot;

namespace Infrastructure.Background
{
    /// <summary>
    /// Фоновый сервис снятия денег только у победителей
    /// </summary>
    public class WithdrawWonBid : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScope;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(30);
        private readonly ILogger<LotAutoCloser> _logger;

        public WithdrawWonBid(IServiceScopeFactory serviceScope, ILogger<LotAutoCloser> logger)
        {
            _serviceScope = serviceScope;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScope.CreateScope();
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    var closedLots = await ctx.Lots.Include(l => l.Bids).ThenInclude(b => b.User)
                            .Where(l => l.Status == LotStatus.Closed && !l.Bids.Any(b => b.Status == BidStatus.Active))
                            .ToListAsync(stoppingToken);

                    foreach (var lot in closedLots)
                    {
                        await ProcessLot(ctx, lot);
                    }

                    await ctx.SaveChangesAsync();

                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error Process bid");
                }

                await Task.Delay(_period, stoppingToken);
            }
        }

        private async Task ProcessLot(AppDbContext ctx, Lot lot)
        {

            var winningBid = ctx.Bids
                    .OrderByDescending(b => b.Amount)
                    .First();

            foreach (var bid in lot.Bids)
            {
                if(bid.Id == winningBid.Id)
                {
                    bid.MarkAsWinning();
                    bid.User.Withdraw(winningBid.Amount);
                    _logger.LogInformation($"{bid.Id} is winning marked for lot {lot.Id}");
                }
                else
                {
                    bid.MarkAsLosing();
                    //bid.User.ReturnMoney(bid.Amount);
                    _logger.LogInformation($"{bid.Id} is losing marked for lot {lot.Id}");
                    //_logger.LogInformation($"Returned {bid.Amount} money {bid.User.Id}");
                }
            }
            
        }
    }
}
