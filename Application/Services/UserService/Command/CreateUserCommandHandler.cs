using Domain.Users;
using Infrastructure.Persistence.Repositores.Users;
using MediatR;
using Registration.JWT;


namespace Application.Services.UserService.Command
{
    /// <summary>
    /// Тело запроса создания пользователя
    /// </summary>
    public class CreateUserCommand : IRequest
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Обработчик запроса на создание пользователя
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRpository;
        private readonly IJWTService _jwt;

        public CreateUserCommandHandler(IUserRepository userRepository, IJWTService jwt)
        {
            _userRpository = userRepository;
            _jwt = jwt;
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
                var (rt, exp) = _jwt.CreateRefreshToken();
                newUser.SetRefreshToken(rt, exp);
            }

        }
    }
}
