using Stml.Application.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Application.Roles
{
    public interface IRoleAppService
    {
        Task<IEnumerable<RoleDto>> GetRolesAsync();
    }
}
