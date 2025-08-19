using Infrastructure.Persistence.Repositores.Users;
using MediatR;


namespace Application.Services.LoginService
{
    public class LogoutCommand : IRequest<Unit>
    {
        public string RefreshToken { get; init; }
    }
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public LogoutCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        async Task<Unit> IRequestHandler<LogoutCommand, Unit>.Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return Unit.Value;
            }
            var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);

            if(user == null)
            {
                return Unit.Value;
            }

            await _userRepository.RevorkeRefreshTokenAsync(user.Id);
            
            return Unit.Value;
        }

    }
}
