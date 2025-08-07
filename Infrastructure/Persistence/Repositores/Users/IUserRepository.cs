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
        Task UpdateUserDataAsync(UpdateUserDataRequest request);
        Task<UserResponse> GetByIdUserAsync(int id);
        Task PlaceBidAsync(User user, Lot lot, long amount);
        Task<bool> CanUserBidOnLotAsync(User user, Lot lot, long amount);
        Task WithdrawWonBidsAsync(User user, Lot lot);
    }
}
