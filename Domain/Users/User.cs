using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class User : BaseEntity
    {
        public string NickName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public long Balance { get; private set; }

        private User()
        {

        }
       
        public User(string nickName, string email, string password)
        {
            NickName = nickName;
            Email = email;
            Password = password;
            Balance = 0;
            SetUpdate();
        }

        #region METHODS
        public void UpdateNickName(string newNick)
        {
            NickName = newNick;
            SetUpdate();
        }

        public void UpdateEmail(string newEmail) { 
            Email = newEmail; 
            SetUpdate(); 
        }

        public void UpdatePassword(string newPassword)
        {
            Password = newPassword;
            SetUpdate();
        }

        #endregion
    }
}
