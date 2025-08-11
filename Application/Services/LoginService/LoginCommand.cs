using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.LoginService
{
    public class LoginCommand : IRequest<(int Id, string Email, string NickName)>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
