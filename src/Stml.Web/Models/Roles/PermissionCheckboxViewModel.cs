using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Models.Roles
{
    public class PermissionCheckboxViewModel
    {
        public PermissionCheckboxViewModel()
        {

        }

        public PermissionCheckboxViewModel(string group, string name, string displayName, bool @checked)
        {
            Group = group;
            Name = name;
            DisplayName = displayName;
            Checked = @checked;
        }

        public string Group { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Checked { get; set; }
    }
}
