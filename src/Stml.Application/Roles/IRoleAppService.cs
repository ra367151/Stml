using Stml.Application.Roles.Dto;
using Stml.Infrastructure.Applications.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Application.Roles
{
    public interface IRoleAppService
    {
        Task<IEnumerable<RoleDto>> GetRolesAsync();
        Task<PagedListDto<RoleDto>> GetRolePagedListAsync(string search, int skip, int take);
        Task DeleteRoleAsync(Guid id);
        Task<RoleDto> FindRoleAsync(Guid id);
        Task<ServiceResult> CreateRoleAsync(RoleCreateInput input);
        Task<ServiceResult> EditRoleAsync(RoleEditInput input);
    }
}
