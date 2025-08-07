using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Bids
{
    internal interface IBidService
    {
        // Создание ставок
        Task<Bid> CreateBidAsync(int userId, int lotId, long amount);

        // Управление статусом ставки
        Task MarkAsWinningAsync(Bid bid);
        Task MarkAsLosingAsync(Bid bid);
        Task CancelBidAsync(Bid bid);

        // Получение данных
        Task<IEnumerable<Bid>> GetUserBidsAsync(int userId);
        Task<IEnumerable<Bid>> GetLotBidsAsync(int lotId);
        Task<Bid?> GetHighestBidAsync(IEnumerable<Bid> bids);
        Task<Bid?> GetByIdAsync(int id);

        // Валидация
        Task<bool> IsValidBidAmountAsync(long amount, long minimumRequired);
        Task<bool> CanUserPlaceBidAsync(int userId, int lotId, long amount);
    }
}
