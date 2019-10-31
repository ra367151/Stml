using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stml.Application.Roles.Dto;
using Stml.Domain.Authorizations;
using Stml.Infrastructure.Applications.Dto;
using Stml.Infrastructure.Applications.Exceptions;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Infrastructure.System.Linq;
using Stml.Infrastructure.System.String;

namespace Stml.Application.Roles
{
    public class RoleAppService : IRoleAppService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleAppService> _logger;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPermissionManager<Permission> _permissionManager;
        private readonly IRoleRepository _roleRepository;

        public RoleAppService(IMapper mapper
            , ILogger<RoleAppService> logger
            , RoleManager<Role> roleManager
            , IPermissionManager<Permission> permissionManager
            , IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _permissionManager = permissionManager;
            _roleRepository = roleRepository;
        }

        public async Task<ServiceResult> CreateRoleAsync(RoleCreateInput input)
        {
            var role = Role.CreateRole(input.Name, input.DisplayName)
                            .SetPermissionsToRole(_permissionManager.Permissions.SelectMany(x => x.Value).Where(x => input.Permissions.Contains(x.Name)).ToArray());
            var identityResult = await _roleManager.CreateAsync(role);
            if (identityResult.Succeeded)
            {
                _logger.LogInformation($"A new role: {input.Name}/{input.DisplayName} created.");
                return ServiceResult.Success;
            }
            return ServiceResult.Fail(identityResult.Errors.Select(x => x.Description).ToArray());
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                throw new UserFriendlyException("用户不存在");
            await _roleManager.DeleteAsync(role);
        }

        public async Task<ServiceResult> EditRoleAsync(RoleEditInput input)
        {
            var role = await _roleRepository.FindRoleIncludeUsersAsync(input.Id);
            if (role == null)
                throw new UserFriendlyException("角色不存在");
            role.UpdateName(input.Name).UpdateDisplayName(input.DisplayName)
                .SetPermissionsToRole(_permissionManager.Permissions.SelectMany(x => x.Value).Where(x => input.Permissions.Contains(x.Name)).ToArray());
            var identityResult = await _roleManager.UpdateAsync(role);
            if (identityResult.Succeeded)
                return ServiceResult.Success;
            return ServiceResult.Fail(identityResult.Errors.Select(x => x.Description).ToArray());
        }

        public async Task<RoleDto> FindRoleAsync(Guid id)
        {
            return _mapper.Map<RoleDto>(await _roleRepository.FindRoleIncludeUsersAsync(id));
        }

        public async Task<PagedListDto<RoleDto>> GetRolePagedListAsync(string search, int skip, int take)
        {
            var count = await _roleManager.Roles
                                        .WhereIf(!search.IsNullOrEmpty(), r => r.Name.Contains(search))
                                        .CountAsync();
            if (count > 0)
            {
                var list = await _roleManager.Roles
                                        .AsNoTracking()
                                        .WhereIf(!search.IsNullOrEmpty(), r => r.Name.Contains(search))
                                        .OrderByDescending(r => r.Name)
                                        .Skip(skip)
                                        .Take(take)
                                        .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
                return new PagedListDto<RoleDto>(count, list);
            }
            return PagedListDto<RoleDto>.Null;
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            return await _roleManager.Roles.ProjectTo<RoleDto>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync();
        }
    }
}
