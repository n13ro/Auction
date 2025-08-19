using Infrastructure.Persistence.Repositores.Users;
using MediatR;


namespace Application.Services.UserService.Command
{
    /// <summary>
    /// Тело запроса закрытия лота
    /// </summary>
    public class CloseLotCommand : IRequest
    {
        public int userId { get; set; }
        public int lotId { get; set; }

    }

    /// <summary>
    /// Обработчик запроса на закрытие лота
    /// </summary>
    public class CloseLotCommandHandler : IRequestHandler<CloseLotCommand>
    {
        private readonly IUserRepository _userRepository;

        public CloseLotCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(CloseLotCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.CloseLotAsync(request.userId, request.lotId);
        }
    }
}
