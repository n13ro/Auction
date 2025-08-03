using Domain.Bids;
using Domain.Common;
using Domain.Lots;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Lots.Lot;

namespace Domain.Users
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public User CreateUser(string nickName, string email, string password)
        {
            ValidateData(nickName, email, password);
            var user = new User(nickName, email, password);
            _logger.LogInformation("Created user with email: {Email}", email);
            return user;
        }

        private void ValidateData(string? nickName, string? email, string? password)
        {
            if (string.IsNullOrWhiteSpace(nickName) && string.IsNullOrEmpty(nickName))
            {
                throw new Exception("Invalid data in nickName");
            }
            if (string.IsNullOrWhiteSpace(email) && email.Contains('@'))
            {
                throw new Exception("Invalid data in email");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Invalid data in password");
            }
        }

        public void UpdateNickName(User user, string newNick)
        {
            if (newNick != user.NickName && newNick.Length >= 4)
            {
                user.UpdateNickName(newNick);
                _logger.LogInformation("Updated nickname for user {UserId} to {NewNick}", user.Id, newNick);
            }
        }

        public void UpdateEmail(User user, string newEmail)
        {
            if (newEmail.Contains('@'))
            {
                user.UpdateEmail(newEmail);
                _logger.LogInformation("Updated email for user {UserId} to {NewEmail}", user.Id, newEmail);
            }
        }

        public void UpdatePassword(User user, string newPassword)
        {
            if (newPassword.Length >= 6)
            {
                user.UpdatePassword(newPassword);
                _logger.LogInformation("Updated password for user {UserId}", user.Id);
            }
        }

        public void Deposit(User user, long amount)
        {
            if (amount > 0 && amount <= 100_000)
            {
                user.Deposit(amount);
                _logger.LogInformation("Deposited {Amount} for user {UserId}", amount, user.Id);
            }
        }

        public void ReturnMoney(User user, long amount)
        {
            if (amount > 0)
            {
                user.ReturnMoney(amount);
                _logger.LogInformation("Returned {Amount} to user {UserId}", amount, user.Id);
            }
        }

        public void Withdraw(User user, long amount)
        {
            if (amount > 0 && amount <= user.Balance)
            {
                user.Withdraw(amount);
                _logger.LogInformation("Withdrew {Amount} from user {UserId}", amount, user.Id);
            }
        }

        public bool CheckBalanceBidOnLot(User user, long amount)
        {
            return user.CheckBalanceBidOnLot(amount);
        }

        public Lot CreateLot(User user, string name, string description,
            long startingPrice, long minBet, bool isExtraTime, TimeSpan lotLife)
        {
            var newLot = new Lot(
                name,
                description,
                startingPrice,
                minBet,
                isExtraTime,
                TimeSpan.FromMinutes(lotLife.Minutes)
            );

            user.Lots.Add(newLot);
            user.UpdateToLastModified();
            _logger.LogInformation("Created lot {LotId} by user {UserId}", newLot.Id, user.Id);
            return newLot;
        }

        public void PlaceBidOnLot(User user, Lot lot, long amount)
        {
            var lastBid = lot.Bids.LastOrDefault();
            var lastBidAmount = lastBid?.Amount ?? lot.StartingPrice;

            if (!CheckBalanceBidOnLot(user, amount) ||
                amount < lot.MinBet ||
                amount <= lastBidAmount)
            {
                throw new Exception("not money");
            }

            user.Withdraw(amount);
            var bid = new Bid(user.Id, lot.Id, amount);
            lot.Bids.Add(bid);
            lot.ExtendTime();
            user.UpdateToLastModified();
            _logger.LogInformation("User {UserId} placed bid {Amount} on lot {LotId}", user.Id, amount, lot.Id);
        }

        public void ReturnBidsToUser(User user, Lot lot)
        {
            var userBids = CallUserBids(user, lot);

            foreach (var bid in userBids)
            {
                ReturnMoney(user, bid.Amount);
            }
            _logger.LogInformation("Returned {BidCount} bids to user {UserId} for lot {LotId}",
                userBids.Count(), user.Id, lot.Id);
        }

        public IEnumerable<Bid> CallUserBids(User user, Lot lot)
        {
            return lot.Bids
                    .Where(b => b.UserId == user.Id)
                    .ToList();
        }

        public void ProcessLotCompletion(User user, Lot lot)
        {
            if (lot.Status == LotStatus.ClosedByUser)
            {
                ReturnBidsToUser(user, lot);
            }

            if (lot.Status != LotStatus.Closed || !lot.Bids.Any())
            {
                return;
            }

            var winningBid = lot.Bids
                .OrderByDescending(b => b.Amount).First();

            if (winningBid.UserId != user.Id)
            {
                ReturnBidsToUser(user, lot);
            }

            if (winningBid.UserId == user.Id)
            {
                var userBids = CallUserBids(user, lot);

                foreach (var bid in userBids)
                {
                    if (bid.Amount != winningBid.Amount)
                    {
                        ReturnMoney(user, bid.Amount);
                    }
                }
            }
            _logger.LogInformation("Processed lot completion for user {UserId} and lot {LotId}", user.Id, lot.Id);
        }

        public void CloseLot(User user, int lotId)
        {
            var thisLot = user.Lots.FirstOrDefault(k => k.Id == lotId);
            if (thisLot != null)
            {
                thisLot?.CloseLot();
                ProcessLotCompletion(user, thisLot);
            }
            user.UpdateToLastModified();
            _logger.LogInformation("User {UserId} closed lot {LotId}", user.Id, lotId);
        }
    }
}
