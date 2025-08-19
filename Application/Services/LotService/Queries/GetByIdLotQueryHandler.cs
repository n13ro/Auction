using Application.Services.BidService.DTOs;
using Application.Services.LotService.DTOs;
using Infrastructure.Persistence.Repositores.Lots;
using MediatR;
using System.Xml.Linq;


namespace Application.Services.LotService.Queries
{
    public class GetByIdLotQuery : IRequest<LotResponse>
    {
        public int LotId { get; set; }
    }

    public class GetByIdLotQueryHandler : IRequestHandler<GetByIdLotQuery, LotResponse>
    {
        private readonly ILotRepository _lotRepository;

        public GetByIdLotQueryHandler(ILotRepository lotRepository)
        {
            _lotRepository = lotRepository;
        }

        public async Task<LotResponse> Handle(GetByIdLotQuery request, CancellationToken cancellationToken)
        {
            var lot = await _lotRepository.GetByIdLotAsync(request.LotId);

            if (lot != null)
            {
                return new LotResponse
                {
                    Id = request.LotId,
                    Name = lot.Name,
                    Description = lot.Description,
                    StartingPrice = lot.StartingPrice,
                    MinBet = lot.MinBet,
                    StartTime = lot.StartTime,
                    EndTime = lot.EndTime,
                    IsExtraTime = lot.IsExtraTime,
                    Status = lot.Status,
                    Bids = lot.Bids
                    .OrderByDescending(k => k.PlacedAt)
                    .Select(k => new BidResponse
                    {
                        Amount = k.Amount,
                        PlacedAt = k.PlacedAt,
                        Status = k.Status,
                        UserId = k.UserId,
                        UserName = k.User.NickName
                    }).ToList()
                };
            }
            else 
            {
                throw new InvalidDataException("Invalid data =)");
            }; 
        }
    }
}
