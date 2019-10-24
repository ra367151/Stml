using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Accounts.Dto
{
    public class UserLoginInput
    {
        public UserLoginInput(string username, string password, bool rememberme)
        {
            UserName = username;
            Password = password;
            RememberMe = rememberme;
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
