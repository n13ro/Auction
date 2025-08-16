using Infrastructure.Persistence.Repositores.Users;
using MediatR;

namespace Application.Services.BidService.Command
{
    /// <summary>
    /// Тело запроса ставки в лоте
    /// </summary>
    public class BidPlacingCommand : IRequest
    { 
        public int userId { get; set; }
        public int lotId { get; set; }
        public long amount { get; set; }
    }

    /// <summary>
    /// Обработчик запроса создания ствки в лоте
    /// </summary>
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
