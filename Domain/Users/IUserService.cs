using Domain.Bids;
using Domain.Lots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public interface IUserService
    {
        // Управление пользователем
        User CreateUser(string nickName, string email, string password);
        Task UpdateNickName(User user, string newNick);
        Task UpdateEmail(User user, string newEmail);
        Task UpdatePassword(User user, string newPassword);

        // Управление балансом
        Task Deposit(User user, long amount);
        Task ReturnMoney(User user, long amount);
        Task Withdraw(User user, long amount);
        bool CheckBalanceBidOnLot(User user, long amount);

        // Управление лотами
        Lot CreateLot(User user, string name, string description,
            long startingPrice, long minBet, bool isExtraTime, TimeSpan lotLife);
        Task CloseLot(User user, int lotId);

        // Управление ставками
        Task PlaceBidOnLot(User user, Lot lot, long amount);
        Task ReturnBidsToUser(User user, Lot lot);
        IEnumerable<Bid> CallUserBids(User user, Lot lot);
        Task ProcessLotCompletion(User user, Lot lot);

    }
}
