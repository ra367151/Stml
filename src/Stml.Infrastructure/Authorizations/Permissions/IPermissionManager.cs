using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public interface IPermissionManager<TPermission> where TPermission : Permission
    {
        IDictionary<string, List<TPermission>> Permissions { get; }
        IEnumerable<TPermission> FindByGroupName(string groupName);
        TPermission Find(string groupName, string name);
        void AddPermission(TPermission permission);
        void Remove(string name);
    }
}
