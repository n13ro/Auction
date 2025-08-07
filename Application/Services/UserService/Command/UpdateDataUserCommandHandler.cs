using Infrastructure.Persistence.Repositores.Users;
using Infrastructure.Persistence.Repositores.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService.Command
{
    public class UpdateDataUserCommandHandler : IRequestHandler<UpdateUserDataCommand>
    {
        private readonly IUserRepository _userRpository;

        public UpdateDataUserCommandHandler(IUserRepository userRepository)
        {
            _userRpository = userRepository;
        }

        public async Task Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRpository.GetByIdUserAsync(request.Id);

            if(user != null)
            {
                if (!string.IsNullOrEmpty(request.NickName))
                {
                    user.NickName = request.NickName;
                }
                if (!string.IsNullOrEmpty(request.Email))
                {
                    user.Email = request.Email;
                }
                if (!string.IsNullOrEmpty(request.Password))
                {
                    user.Password = request.Password;
                }
            }
            var updateRequest = new UpdateUserDataRequest
            {
                Id = request.Id,
                NickName = request.NickName,
                Email = request.Email,
                Password = request.Password,

            };
            await _userRpository.UpdateUserDataAsync(updateRequest);
        }
    }
}
