using Domain.Lots;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositores.Lots
{
    public interface ILotRepository
    {
        Task<Lot> CreateLotAsync(User user, string name, string description, long startingPrice,
           long minBet, bool isExtraTime, TimeSpan lotLife);

        Task CloseLotAsync(Lot lot);
        Task CloseLotByUserAsync(Lot lot);
    }
}
