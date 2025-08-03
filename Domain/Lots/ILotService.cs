using Domain.Bids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Lots
{
    public interface ILotService
    {
        Task<Lot> CreateLotAsync(string name, string description, long startingPrice,
            long minBet, bool isExtraTime, TimeSpan lotLife);
        Task CloseLotAsync(Lot lot);
        Task CloseLotByUserAsync(Lot lot);
        Task ExtendTimeAsync(Lot lot);

        // Проверки
        Task<bool> IsActiveAsync(Lot lot);

        // Управление ставками
        Task AddBidAsync(Lot lot, Bid bid);
        Task<IEnumerable<Bid>> GetBidsAsync(Lot lot);
        Task<Bid?> GetWinningBidAsync(Lot lot);

        // Получение лотов
        Task<Lot?> GetByIdAsync(int id);
        Task<IEnumerable<Lot>> GetActiveLotsAsync();
        Task<IEnumerable<Lot>> GetLotsByUserAsync(int userId);
    }
}
