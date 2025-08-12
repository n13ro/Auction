using Domain.Users;
using Infrastructure.Persistence.Repositores.Users;
using MediatR;


namespace Application.Services.UserService.Command
{
    public class CreateUserCommand : IRequest
    {

        public int Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
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
                await _userRpository.CreateUserAsync(newUser);
            }

        }
    }
}
