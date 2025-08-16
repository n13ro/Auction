using Infrastructure.Persistence.Repositores.Users;
using MediatR;


namespace Application.Services.UserService.Command
{
    /// <summary>
    /// Тело запроса пополнение счета
    /// </summary>
    public class DepositOnBalanceCommand : IRequest
    {
        public int Id { get; set; }
        public long Amount {  get; set; }
    }

    /// <summary>
    /// Обработчик запроса на пополнение счета
    /// </summary>
    public class DepositOnBalanceCommandHandler : IRequestHandler<DepositOnBalanceCommand>
    {
        private readonly IUserRepository _userRepository;

        public DepositOnBalanceCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DepositOnBalanceCommand request, CancellationToken cancellationToken)
        { 
            var user = await _userRepository.GetByIdUserAsync(request.Id);
            await _userRepository.DepositOnBalanceAsync(user.Id, request.Amount);
        }
    }
}
