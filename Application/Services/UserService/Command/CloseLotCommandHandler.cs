using Infrastructure.Persistence.Repositores.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService.Command
{
    public class CloseLotCommand : IRequest
    {
        public int userId { get; set; }
        public int lotId { get; set; }

    }
    public class CloseLotCommandHandler : IRequestHandler<CloseLotCommand>
    {
        private readonly IUserRepository _userRepository;

        public CloseLotCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(CloseLotCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.CloseLotAsync(request.userId, request.lotId);
        }
    }
}
