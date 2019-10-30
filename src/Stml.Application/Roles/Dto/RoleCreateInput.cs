using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Roles.Dto
{
    public class RoleCreateInput
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        /// <summary>
        /// Permission's name, not display name！
        /// </summary>
        public string[] Permissions { get; set; }
    }
}
