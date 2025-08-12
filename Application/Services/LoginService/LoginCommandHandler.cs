using Infrastructure.Persistence.Repositores.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.LoginService
{
    public class LoginCommand : IRequest<(int Id, string Email, string NickName)>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, (int Id, string Email, string NickName)>
    {
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(int Id, string Email, string NickName)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailUserAsync(request.Email);
            if(user is null)
            {
                throw new UnauthorizedAccessException("Invalid data");
            }

            if(user.Password != request.Password)
            {
                throw new UnauthorizedAccessException("Invalid data");
            }
            return (user.Id, user.Email, user.NickName);
        }
    }
}
