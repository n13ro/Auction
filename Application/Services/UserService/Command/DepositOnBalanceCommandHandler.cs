using Infrastructure.Persistence.Repositores.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService.Command
{
    public class DepositOnBalanceCommand : IRequest
    { 
        public int id { get; set; }
        public long amount { get; set; }
    }

    public class DepositOnBalanceCommandHandler : IRequestHandler<DepositOnBalanceCommand>
    {
        private readonly IUserRepository _userRepository;

        public DepositOnBalanceCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DepositOnBalanceCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdUserAsync(request.id);
            await _userRepository.DepositOnBalanceAsync(user.Id, request.amount);
        }
    }
}
