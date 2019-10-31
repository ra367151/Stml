using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Users.Dto
{
    public class UserCreateInput
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// role name array. not display name!
        /// </summary>
        public string[] Roles { get; set; }
    }
}
