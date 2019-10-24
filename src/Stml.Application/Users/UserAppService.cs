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
using Stml.Application.Users.Dto;
using Stml.Domain.Roles;
using Stml.Domain.Users;
using Stml.Infrastructure.Applications.Dto;
using Stml.Infrastructure.Applications.Exceptions;
using Stml.Infrastructure.Collection.Extensions;
using Stml.Infrastructure.Extensions;
using Stml.Infrastructure.Linq.Extensions;

namespace Stml.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserAppService> _logger;

        public UserAppService(UserManager<User> userManager
            , RoleManager<Role> roleManager
            , IMapper mapper
            , ILogger<UserAppService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult> CreateUserAsync(UserCreateInput input)
        {
            var user = new User { UserName = input.UserName, Email = input.Email };
            var identityResult = await _userManager.CreateAsync(input.IsActive ? user.UpdateToActive() : user.UpdateToUnActive(), input.Password);
            if (identityResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, input.Roles);
                _logger.LogInformation($"A new account: {input.UserName} created with password: {input.Password}.");
                return ServiceResult.Success;
            }
            return ServiceResult.Fail(identityResult.Errors.Select(x => x.Description).ToArray());
        }

        public async Task<PagedListDto<UserDto>> GetUserPagedListAsync(string queryString, int skip, int take)
        {
            var count = await _userManager.Users
                                .WhereIf(!queryString.IsNullOrEmpty(), u => u.UserName.Contains(queryString.Trim()) || u.Email.Contains(queryString.Trim()))
                                .CountAsync();
            if (count > 0)
            {
                var list = await _userManager.Users
                                    .WhereIf(!queryString.IsNullOrEmpty(), u => u.UserName.Contains(queryString.Trim()) || u.Email.Contains(queryString.Trim()))
                                    .OrderByDescending(u => u.CreationTime)
                                    .Skip(skip)
                                    .Take(take)
                                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
                return new PagedListDto<UserDto>(count, list);
            }
            return PagedListDto<UserDto>.Null;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<UserDto> FindUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var dto = _mapper.Map<UserDto>(user);
            if (user != null)
            {
                dto.Roles = (await _userManager.GetRolesAsync(user)).ToArray();
            }
            return dto;
        }

        public async Task<ServiceResult> EditUserAsync(UserEditInput input)
        {
            var user = await _userManager.FindByIdAsync(input.Id.ToString());
            if (user == null)
                throw new UserFriendlyException("用户不存在");
            user.UpdateUserName(input.UserName).UpdateEmai(input.Email);
            if (input.IsActive)
                user.UpdateToActive();
            else
                user.UpdateToUnActive();
            var identityResult = await _userManager.UpdateAsync(user);
            if (identityResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles.Except(input.Roles));
                await _userManager.AddToRolesAsync(user, input.Roles.Except(userRoles));
                return ServiceResult.Success;
            }
            return ServiceResult.Fail(identityResult.Errors.Select(x => x.Description).ToArray());
        }
    }
}
