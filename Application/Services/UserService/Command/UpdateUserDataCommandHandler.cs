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
    #region UpdateData Service
    public class UpdateUserDataCommand : IRequest
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    #endregion
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
                    user.UpdateNickName(request.NickName);
                }
                if (string.IsNullOrEmpty(request.Email))
                {
                    user.UpdateEmail(request.Email);
                }
                if (string.IsNullOrEmpty(request.Password))
                {
                    user.UpdatePassword(request.Password);
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
