using Infrastructure.Persistence.Repositores.Users;
using MediatR;
using Registration.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.LoginService
{
    public class RefreshTokenCommand : IRequest<TokenReponse>
    {
        public string RefreshToken {  get; init; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenReponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwt;

        public RefreshTokenCommandHandler(IUserRepository userRepository, IJWTService jwt)
        {
            _userRepository = userRepository;
            _jwt = jwt;
        }

        public async Task<TokenReponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);

            if (user is null || user.RefreshToken is null || user.RefreshTokenLife is null || user.RefreshTokenLife <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid =D");
            }

            var access = _jwt.CreateToken(user.Id, user.Email, user.NickName);
            var (rt, exp) = _jwt.CreateRefreshToken();

            await _userRepository.SetRefreshTokenAsync(user.Id, rt, exp);

            return new TokenReponse(access, rt);
        }
    }
}
