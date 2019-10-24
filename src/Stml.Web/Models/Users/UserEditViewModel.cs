using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Models.Users
{
    public class UserEditViewModel
    {
        public const int UserNameMinLength = 4;
        public const int UserNameMaxLength = 15;

        public Guid Id { get; set; }

        [Display(Name = "用户名")]
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; }

        [Display(Name = "邮箱")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "启用")]
        public bool IsActive { get; set; }

        [Display(Name = "角色")]
        public List<RoleCheckboxViewModel> Roles { get; set; }
    }
}
