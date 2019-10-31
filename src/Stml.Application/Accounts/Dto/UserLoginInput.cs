using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Accounts.Dto
{
    public class UserLoginInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
