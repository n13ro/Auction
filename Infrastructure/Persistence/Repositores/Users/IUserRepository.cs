using Domain.Bids;
using Domain.Lots;
using Domain.Users;
using Infrastructure.Persistence.Repositores.Users.DTOs;


namespace Infrastructure.Persistence.Repositores.Users
{
    public interface IUserRepository
    {

        //Специф. операции
        Task CreateUserAsync(User user);
        Task UpdateUserDataAsync(UpdateUserDataRequest req);
        Task<User> GetByIdUserAsync(int id);
        Task<User> GetByEmailUserAsync(string email);
        Task PlaceBidAsync(int userId, int lotId, long amount);
        Task<bool> CanUserBidOnLotAsync(int userId, int lotId, long amount);
        Task WithdrawWonBidsAsync(int userId, int lotId);
        Task DepositOnBalanceAsync(int id, long amount);
    }
}
