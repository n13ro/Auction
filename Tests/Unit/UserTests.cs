using Domain.Lots;
using Domain.Users;


namespace Tests.Unit
{
    public class UserTests
    {
        private readonly UserService _userService;
        private readonly TimeSpan timeLifeLot = new TimeSpan(0, 30, 0);

        public UserTests(UserService userService)
        {
            _userService = userService;
        }

        [Fact]
        public void DepositOnUser()
        {
            User user1 = _userService.CreateUser("User1", "sdfsdfs@sdfsd", "dsfsfsfk");
            Assert.NotNull(user1);
            Assert.Equal(0, user1.Balance);

            user1.Deposit(100_000);
            Assert.Equal(100_000, user1.Balance);
        }

        [Fact]
        public void DepositOnUser_InvalidAmount_ShouldNotChangeBalance()
        {
            User user2 = _userService.CreateUser("User2", "sdfsdfs@sdfsd", "dsfsfsfk");
            var initBal = user2.Balance;
            user2.Deposit(-100);
            Assert.Equal(initBal, user2.Balance);
        }

        [Fact]
        public void DepositOnUser_ExceedMaxAmount_ShouldNotChangeBalance()
        {
            User user2 = _userService.CreateUser("User2", "sdfsdfs@sdfsd", "dsfsfsfk");
            var initialBalance = user2.Balance;
            user2.Deposit(150_000);
            Assert.Equal(initialBalance, user2.Balance);
        }

        [Fact]
        public void PlaceBidOnLot_ValidBid_ShouldAddBid()
        {
            User user1 = _userService.CreateUser("User1", "sdfsdfs@sdfsd", "dsfsfsfk");
            User user2 = _userService.CreateUser("User2", "sdfsdfs@sdfsd", "dsfsfsfk");

            Lot lot = _userService.CreateLot(user1, "adad", "sdfsdfsd", 1000, 100, false, timeLifeLot);

            user1.Deposit(50_000);
            user2.Deposit(50_000);

            user2.AddLot(lot);
            user1.AddLot(lot);
            user2.AddLot(lot);

            _ = _userService.PlaceBidOnLot(user2, lot, 1100);
            _ = _userService.PlaceBidOnLot(user1, lot, 1200);
            _ = _userService.PlaceBidOnLot(user2, lot, 1300);

            Assert.Equal(48_800, user1.Balance);
            Assert.Equal(47_600, user2.Balance);
            Assert.Equal(1100, lot.Bids.First().Amount);
            //Assert.Equal(1200, lot.Bids.LastOrDefault()?.Amount);
            Assert.Equal(1300, lot.Bids.LastOrDefault()?.Amount);

        }

    }
}
