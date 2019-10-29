using Stml.Infrastructure.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public class PermissionManager<TPermission> : IPermissionManager<TPermission>
        where TPermission : Permission
    {
        private readonly IDictionary<string, List<TPermission>> permissions;

        public PermissionManager()
        {
            permissions = new Dictionary<string, List<TPermission>>();
        }

        public IDictionary<string, List<TPermission>> Permissions => permissions;

        public void AddPermission(TPermission permission)
        {
            Check.NotNull(permission, nameof(permission));
            if (permission.Obsolete)
                return;
            if (Permissions.ContainsKey(permission.Group))
            {
                if (permissions[permission.Group].Any(p => p.Name == permission.Name))
                    throw new InvalidOperationException($"PermissionGroup: {permission.Group} already has Permission: {permission.Name}");

                permissions[permission.Group].Add(permission);
            }
            else
            {
                permissions.Add(permission.Group, new List<TPermission> { permission });
            }
        }

        public TPermission Find(string groupName, string name)
        {
            return permissions[groupName]?.SingleOrDefault(p => p.Name == name);
        }

        public IEnumerable<TPermission> FindByGroupName(string groupName)
        {
            return permissions[groupName];
        }

        public void Remove(string name)
        {
            foreach (var ps in permissions.Values)
            {
                ps.RemoveAll(p => p.Name == name);
            }
        }
    }
}
