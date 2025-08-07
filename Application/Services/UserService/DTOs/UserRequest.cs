using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService.DTOs
{
    public class UserRequest
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Balance { get; set; }
    }
}
