using Domain.Users;
using Infrastructure.Persistence.Repositores.Users;
using MediatR;


namespace Application.Services.UserService.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRpository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRpository = userRepository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User(
                request.NickName,
                request.Email, 
                request.Password);

            if (request != null)
            {
                await _userRpository.CreateUser(newUser);
            }

        }
    }
}
