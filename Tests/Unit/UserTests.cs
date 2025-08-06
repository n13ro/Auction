using Domain.Lots;
using Domain.Users;
using FluentAssertions;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositores.Users;
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
        public async Task CheckBalanceBidOnLot_Test(long userBalance, long bidAmount, bool expectedResult)
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
        [InlineData(1000, true)]
        [InlineData(-200, false)]
        [InlineData(100_111, false)]
        [InlineData(0, false)]
        [InlineData(100_001, false)]
        public void DepositMax_Test(long amount, bool isSuccess)
        {
            // Arrange
            var user = _mocker.CreateInstance<User>();
            var initBalance = user.Balance;


            if(isSuccess)
            {
                user.Deposit(amount);
                user.Balance.Should().Be(initBalance + amount);
            }
            else
            {
                Assert.Fail("Error values ");
            }
        }

    }
}
