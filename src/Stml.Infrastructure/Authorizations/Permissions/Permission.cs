using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public class Permission
    {
        public string Group { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public bool Obsolete { get; private set; }

        public Permission([NotNull]string group, [NotNull]string name, string displayName = null, bool obsolete = false)
        {
            Check.NotNullOrEmpty(group, nameof(group));
            Check.NotNullOrEmpty(name, nameof(name));
            Group = group;
            Name = name;
            DisplayName = displayName ?? name;
            Obsolete = obsolete;
        }
    }
}
