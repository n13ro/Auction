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
        void UpdateNickName(User user, string newNick);
        void UpdateEmail(User user, string newEmail);
        void UpdatePassword(User user, string newPassword);

        // Управление балансом
        void Deposit(User user, long amount);
        void ReturnMoney(User user, long amount);
        void Withdraw(User user, long amount);
        bool CheckBalanceBidOnLot(User user, long amount);

        // Управление лотами
        Lot CreateLot(User user, string name, string description,
            long startingPrice, long minBet, bool isExtraTime, TimeSpan lotLife);
        void CloseLot(User user, int lotId);

        // Управление ставками
        void PlaceBidOnLot(User user, Lot lot, long amount);
        void ReturnBidsToUser(User user, Lot lot);
        IEnumerable<Bid> CallUserBids(User user, Lot lot);
        void ProcessLotCompletion(User user, Lot lot);

    }
}
