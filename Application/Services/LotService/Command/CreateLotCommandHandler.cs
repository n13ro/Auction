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

namespace Application.Services.LotService.Command
{
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
