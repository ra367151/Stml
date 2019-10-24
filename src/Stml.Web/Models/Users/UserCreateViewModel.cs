using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Models.Users
{
    public class UserCreateViewModel
    {
        public const int UserNameMinLength = 4;
        public const int UserNameMaxLength = 15;

        // identity password default regex: ^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$
        // (?=.*[a-z]) : Should have at least one lower case
        // (?=.*[A-Z]) : Should have at least one upper case
        // (?=.*\d) : Should have at least one number
        // (?=.*[#$^+=!*()@%&] ) : Should have at least one special character
        // .{8,} : Minimum 8 characters
        public const string PasswordRegularExpression = @"^(?=.*[a-zA-Z])(?=.*\d).{6,}$";


        [Display(Name = "用户名")]
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; }

        [Display(Name = "邮箱")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "密码")]
        [Required]
        [RegularExpression(PasswordRegularExpression)]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "启用")]
        public bool IsActive { get; set; }

        [Display(Name = "角色")]
        public List<RoleCheckboxViewModel> Roles { get; set; }
    }
}
