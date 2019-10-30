using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Models.Roles
{
    public class RoleCreateViewModel
    {
        public const int RoleNameMinLength = 3;
        private const int RoleNameMaxLength = 10;
        private const int RoleDisplayNameMinLength = 2;
        private const int RoleDisplayNameMaxLength = 10;

        [Display(Name = "名称")]
        [StringLength(RoleNameMaxLength, MinimumLength = RoleNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "显示名称")]
        [StringLength(RoleDisplayNameMaxLength, MinimumLength = RoleDisplayNameMinLength)]
        public string DisplayName { get; set; }

        [Display(Name = "权限")]
        public List<PermissionCheckboxViewModel> Permissions { get; set; }
    }
}
