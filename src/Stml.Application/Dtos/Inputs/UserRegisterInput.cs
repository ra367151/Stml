using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stml.Application.Dtos.Inputs
{
    public class UserRegisterInput
    {
        [Display(Name = "账号")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
