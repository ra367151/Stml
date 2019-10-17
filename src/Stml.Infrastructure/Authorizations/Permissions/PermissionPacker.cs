using Stml.Infrastructure.Json.Extensions;
using Stml.Infrastructure.Security.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    public class PermissionPacker : IPermissionPacker
    {
        //public const char PackType = 'H';
        //public const int PackSize = 4;

        //private static string DefaultPackPrefix()
        //{
        //    return $"{PackType}{PackSize:D1}-";
        //}

        public readonly IStringEncryptionService _stringEncryptionService;
        public PermissionPacker(IStringEncryptionService stringEncryptionService)
        {
            _stringEncryptionService = stringEncryptionService;
        }

        public string PackPermissionsIntoString(IEnumerable<Permission> permissions)
        {
            //return permissions.Aggregate(DefaultPackPrefix(), (s, permission) => s + permission.Name);

            return _stringEncryptionService.Encrypt(permissions.ToJsonString());
        }

        public IEnumerable<Permission> UnPackPermissionFromString(string packedPermissions)
        {
            return _stringEncryptionService.Decrypt(packedPermissions).FromJsonString<IEnumerable<Permission>>();
        }
    }
}
