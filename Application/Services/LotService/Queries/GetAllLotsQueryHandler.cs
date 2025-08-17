using Application.Services.LotService.DTOs;
using Domain.Lots;
using Infrastructure.Persistence.Repositores.Lots;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.LotService.Queries
{
    public class GetAllLotsQuery : IRequest<IEnumerable<LotResponse>>;

    public class GetAllLotsQueryHandler : IRequestHandler<GetAllLotsQuery, IEnumerable<LotResponse>>
    {
        private readonly ILotRepository _lotRepository;

        public GetAllLotsQueryHandler(ILotRepository lotRepository)
        {
            _lotRepository = lotRepository;
        }

        public async Task<IEnumerable<LotResponse>> Handle(GetAllLotsQuery request, CancellationToken cancellationToken)
        {
            var all = await _lotRepository.GetAllAsync();
            return all.Select(lot => new LotResponse
            {
                Name = lot.Name,
                Description = lot.Description,
                StartingPrice = lot.StartingPrice,
                MinBet = lot.MinBet,
                StartTime = lot.StartTime,
                EndTime = lot.EndTime,
                IsExtraTime = lot.IsExtraTime,
                Status = lot.Status
            });
        }
    }
}
