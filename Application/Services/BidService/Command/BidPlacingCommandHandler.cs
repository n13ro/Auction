using Infrastructure.Persistence.Repositores.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BidService.Command
{
    public class BidPlacingCommand: IRequest
    {
        public int userId { get; set; }
        public int lotId {  get; set; }
        public long amount { get; set; }
    }
    public class BidPlacingCommandHandler : IRequestHandler<BidPlacingCommand>
    {
        private readonly IUserRepository _userRepository;
        public BidPlacingCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(BidPlacingCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.PlaceBidAsync(request.userId, request.lotId, request.amount);
        }
    }
}
