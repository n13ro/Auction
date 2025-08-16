using Domain.Lots;
using Domain.Users;
using Domain.Bids;
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
    public class WithdrawWonBid : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScope;
        private readonly TimeSpan _period = TimeSpan.FromSeconds(30);
        private readonly ILogger _logger;
        public WithdrawWonBid(IServiceScopeFactory serviceScope, ILogger<LotAutoCloser> logger)
        {
            _serviceScope = serviceScope;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var currentTime = DateTime.UtcNow;
                    using var scope = _serviceScope.CreateScope();
                    var _ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var closedLots = await _ctx.Lots
                        .Include(l => l.Bids).ThenInclude(b => b.user)
                        .Where(l => l.Status == LotStatus.Closed
                        && !l.Bids.Any(b => b.Status == Bid.BidStatus.Active))
                        .ToListAsync(stoppingToken);


                    foreach (var lot in closedLots)
                    {
                        await ProcessLot(_ctx, lot);
                    }

                    await _ctx.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex) { }

                await Task.Delay(_period, stoppingToken);
            }
        }

        private async Task ProcessLot(AppDbContext _ctx, Lot lot)
        {
            var winningBid =  _ctx.Bids
                    .OrderByDescending(b => b.Amount)
                    .First();

            foreach(var bid in lot.Bids)
            {
                if(bid.Id == winningBid.Id)
                {
                    bid.MarkAsWinning();
                    bid.user.Withdraw(winningBid.Amount);
                }
                else
                {
                    bid.MarkAsLosing();
                    //bid.user.ReturnMoney(bid.Amount);
                }
            }


            //if (!lot.IsActive && lot.Status != LotStatus.ClosedByUser)
            //{

            //    if (winningBid.userId == user.Id)
            //    {
            //        user.Withdraw(winningBid.Amount);
            //    }
            //}
        }
    }
}
