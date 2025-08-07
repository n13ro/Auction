using Domain.Users;
using Infrastructure.Persistence.Repositores.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(CreateUserCommand request, 
            CancellationToken cancellationToken)
        {
            var newUser = new User(
                request.NickName,
                request.Email,
                request.Password);

            if (request != null)
            {
                await _userRepository.CreateUserAsync(newUser);

            }

        }
    }
}
