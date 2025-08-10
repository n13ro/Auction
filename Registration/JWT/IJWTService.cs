using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.JWT
{
    public interface IJWTService
    {
        string CreateToken(int userId, string email, string nickName);
    }
}
