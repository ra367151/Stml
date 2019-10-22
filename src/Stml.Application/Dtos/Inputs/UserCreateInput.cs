using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stml.Application.Dtos.Inputs
{
    public class UserCreateInput
    {
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "启用")]
        public bool IsEnable { get; set; }
    }
}
