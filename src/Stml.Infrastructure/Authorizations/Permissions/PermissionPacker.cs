using Stml.Infrastructure.System.Json;
using Stml.Infrastructure.System.Security.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public class PermissionPacker : IPermissionPacker
    {
        public readonly IStringEncryptionService _stringEncryptionService;
        public PermissionPacker(IStringEncryptionService stringEncryptionService)
        {
            _stringEncryptionService = stringEncryptionService;
        }

        public string PackPermissionsIntoString(IEnumerable<Permission> permissions)
        {
            return _stringEncryptionService.Encrypt(permissions.ToJsonString());
        }

        public IEnumerable<Permission> UnPackPermissionFromString(string packedPermissions)
        {
            return _stringEncryptionService.Decrypt(packedPermissions).FromJsonString<IEnumerable<Permission>>();
        }
    }
}
