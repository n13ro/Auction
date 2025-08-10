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
    public class UpdateUserDataCommandHandler : IRequestHandler<UpdateUserDataCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserDataCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdUserAsync(request.Id);
            
            if(user != null)
            {
                if (string.IsNullOrEmpty(request.NickName))
                {
                    user.NickName = request.NickName;
                }
                if (string.IsNullOrEmpty(request.Email))
                {
                    user.Email = request.Email;
                }
                if (string.IsNullOrEmpty(request.Password))
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
            await _userRepository.UpdateUserDataAsync(updateRequest);
        }
    }
}
