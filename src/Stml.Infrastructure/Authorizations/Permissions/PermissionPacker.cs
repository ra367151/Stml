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

        public string PackPermissionsIntoString<TPermission>(IEnumerable<TPermission> permissions) where TPermission : Permission
        {
            return _stringEncryptionService.Encrypt(permissions.ToJsonString());
        }

        public IEnumerable<TPermission> UnPackPermissionFromString<TPermission>(string packedPermissions) where TPermission : Permission
        {
            return _stringEncryptionService.Decrypt(packedPermissions).FromJsonString<IEnumerable<TPermission>>();
        }
    }
}
