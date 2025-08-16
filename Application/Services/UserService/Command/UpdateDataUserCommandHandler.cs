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
    /// <summary>
    /// Тело запроса обновления данных
    /// </summary>
    public class UpdateUserDataCommand : IRequest
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Обработчик запроса на обновление данных пользователя
    /// </summary>
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
                    user.UpdateNickName(request.NickName);
                }
                if (!string.IsNullOrEmpty(request.Email))
                {
                    user.UpdateEmail(request.Email);
                }
                if (!string.IsNullOrEmpty(request.Password))
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
            await _userRpository.UpdateUserDataAsync(updateRequest);
        }
    }
}
