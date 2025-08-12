using Domain.Lots;
using Infrastructure.Persistence.Repositores.Lots;
using Infrastructure.Persistence.Repositores.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Domain.Lots.Lot;

namespace Application.Services.LotService.Command
{

    public class CreateLotCommand : IRequest
    {
        public int userId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long StartingPrice { get; set; }
        public long MinBet { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsExtraTime { get; set; }
        public LotStatus Status { get; set; } = LotStatus.Active;
        public double LotLife { get; set; }
    }

    public class CreateLotCommandHandler : IRequestHandler<CreateLotCommand>
    {
        private readonly ILotRepository _lotRepostiry;

        public CreateLotCommandHandler(
            ILotRepository lotRepository)
        {
            _lotRepostiry = lotRepository;
        }

        public async Task Handle(CreateLotCommand request, 
            CancellationToken cancellationToken)
        {
            if (request != null)
            {
                await _lotRepostiry.CreateLotAsync(
                    request.userId, request.Name, 
                    request.Description, request.StartingPrice, 
                    request.MinBet, request.IsExtraTime, 
                    request.LotLife);
            }

        }
    }
}
