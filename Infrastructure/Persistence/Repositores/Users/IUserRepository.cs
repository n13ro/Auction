using Domain.Bids;
using Domain.Lots;
using Domain.Users;


namespace Infrastructure.Persistence.Repositores.Users
{
    public interface IUserRepository
    {

        //Специф. операции
        Task PlaceBidAsync(User user, Lot lot, long amount);
        Task<bool> CanUserBidOnLotAsync(User user, Lot lot, long amount);
        Task WithdrawWonBidsAsync(User user, Lot lot);
    }
}
