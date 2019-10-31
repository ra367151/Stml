using AutoMapper;
using Stml.Application.Accounts.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Models.Accounts
{
    [AutoMap(typeof(UserLoginInput), ReverseMap = true)]
    public class UserLoginViewModel
    {
        [Display(Name = "账号")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "记住密码")]
        public bool RememberMe { get; set; }
    }
}
