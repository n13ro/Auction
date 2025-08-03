using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitUsers
{
    public class UserTests
    {
        public UserService UserdService;

        private TimeSpan timeLifeLot = new TimeSpan(0, 30, 0);
        private User user1 = new User("User1", "sdfsdfs@sdfsd", "dsfsfsfk");
        private User user2 = new User("User2", "sdfsdfs@sdfsd", "dsfsfsfk");

        [Fact]
        public void DepositOnUser()
        {
            Assert.NotNull(user1);
            Assert.Equal(0, user1.Balance);

            user1.Deposit(100_000);
            Assert.Equal(100_000, user1.Balance);
        }

        [Fact]
        public void DepositOnUser_InvalidAmount_ShouldNotChangeBalance()
        {
            var initBal = user2.Balance;
            user2.Deposit(-100);
            Assert.Equal(initBal, user2.Balance);
        }

        [Fact]
        public void DepositOnUser_ExceedMaxAmount_ShouldNotChangeBalance()
        {
            var initialBalance = user2.Balance;
            user2.Deposit(150_000);
            Assert.Equal(initialBalance, user2.Balance);
        }

        [Fact]
        public void PlaceBidOnLot_ValidBid_ShouldAddBid()
        {
            var lot = user1.CreateLot(
                "name", 
                "fsdfs", 
                1000, 
                100, 
                false, 
                timeLifeLot);

            user1.Deposit(50_000);
            user2.Deposit(50_000);

            user2.PlaceBidOnLot(lot, 1100);
            user1.PlaceBidOnLot(lot, 1200);
            user2.PlaceBidOnLot(lot, 1300);

            Assert.Equal(48_800, user1.Balance);
            Assert.Equal(47_600, user2.Balance);
            Assert.Equal(1100, lot.Bids.First().Amount);
            //Assert.Equal(1200, lot.Bids.LastOrDefault()?.Amount);
            Assert.Equal(1300, lot.Bids.LastOrDefault()?.Amount);

        }
        [Fact]
        public void PlaceBidOnLot_ValidAuction()
        {
            var lot = user1.CreateLot(
                "name",
                "fsdfs",
                1000,
                100,
                false,
                timeLifeLot);
            

            user2.Deposit(5000);
            user1.Deposit(5000);

            user2.PlaceBidOnLot(lot, 1100);
            Assert.Equal(3900, user2.Balance);

            user1.PlaceBidOnLot(lot, 1200);
            Assert.Equal(3800, user1.Balance);

            lot.CloseLot();
        }
    }
}
