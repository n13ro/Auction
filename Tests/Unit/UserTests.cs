using Domain.Lots;
using Domain.Users;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;


namespace Tests.Unit
{
    public class UserTests
    {
        private readonly AutoMocker _mocker;
        private readonly TimeSpan timeLifeLot = new(0, 30, 0);

        public UserTests()
        {
            _mocker = new AutoMocker();
        }

        [Theory]
        [InlineData(1000, 100, true)]
        [InlineData(50, 100, false)]
        [InlineData(0, 100, false)]
        public async Task CheckBalanceBidOnLot_Test(
            long userBalance, long bidAmount, bool expectedResult)
        {
            var user = _mocker.CreateInstance<User>();
            user.Deposit(userBalance);

            var result = user.CheckBalanceBidOnLot(bidAmount);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1000, 100, 900)]
        [InlineData(500, 200, 300)]
        [InlineData(100, 50, 50)]
        public void Withdraw_Test(long initialBalance, long withdrawAmount, long expectedBalance)
        {
            // Arrange
            var user = _mocker.CreateInstance<User>();
            user.Deposit(initialBalance);

            // Act
            user.Withdraw(withdrawAmount);

            // Assert
            user.Balance.Should().Be(expectedBalance);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(-200)]
        [InlineData(100_111)]
        [InlineData(0)]
        [InlineData(100_001)]
        [InlineData(null)]
        public void DepositMax_Test(long amount)
        {
            // Arrange
            var user = _mocker.CreateInstance<User>();
            var initBalance = user.Balance;


            if(amount > 0 && amount <= 100_000)
            {
                user.Deposit(amount);
                user.Balance.Should().Be(initBalance + amount);
            }
            else if (amount < 0 || amount > 100_000)
            {
                Assert.Fail($"Error values: hight or low");
            }
            else
            {
                Assert.Fail($"Error values {amount}");
            }
        }

    }
}
