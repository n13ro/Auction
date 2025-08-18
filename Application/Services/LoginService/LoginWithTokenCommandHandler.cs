using Infrastructure.Persistence.Repositores.Users;
using MediatR;
using Registration.JWT;

namespace Application.Services.LoginService
{
    public record TokenReponse(string AccessToken, string RefreshToken);

    /// <summary>
    /// Тело запроса входа в аккаунт
    /// </summary>
    public class LoginWithTokenCommand : IRequest<TokenReponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Обработчик запроса на вход в аккаунт
    /// </summary>
    public class LoginWithTokenCommandHandler : IRequestHandler<LoginWithTokenCommand, TokenReponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwt;

        public LoginWithTokenCommandHandler(IUserRepository userRepository, IJWTService jwt)
        {
            _userRepository = userRepository;
            _jwt = jwt;
        }

        public async Task<TokenReponse> Handle(LoginWithTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailUserAsync(request.Email);

            if(user is null || user.Password != request.Password)
            {
                throw new UnauthorizedAccessException("Invalid data");
            }
            
            if(user.RefreshToken is null || user.RefreshTokenLife is null || user.RefreshTokenLife < DateTime.UtcNow)
            {
                var accees = _jwt.CreateToken(user.Id, request.Email, request.Password); 
                var (rt, exp) = _jwt.CreateRefreshToken();
                await _userRepository.SetRefreshTokenAsync(user.Id, rt, exp);
                return new TokenReponse(accees, rt);
            }
            else
            {
                throw new InvalidDataException("Invalid data");
            }
            
        }
    }
}
