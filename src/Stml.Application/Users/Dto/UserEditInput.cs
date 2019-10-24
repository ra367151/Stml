using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stml.Application.Users.Dto
{
    public class UserEditInput
    {
        public Guid Id { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Display(Name = "启用")]
        public bool IsActive { get; set; }

        [Display(Name = "角色")]
        public string[] Roles { get; set; }
    }
}
