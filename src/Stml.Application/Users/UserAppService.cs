using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stml.Application.Users.Dto;
using Stml.Domain.Authorizations;
using Stml.Infrastructure.Applications.Dto;
using Stml.Infrastructure.Applications.Exceptions;
using Stml.Infrastructure.Extensions;
using Stml.Infrastructure.Linq.Extensions;

namespace Stml.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserAppService> _logger;

        public UserAppService(IMapper mapper
            , ILogger<UserAppService> logger
            , UserManager<User> userManager
            , RoleManager<Role> roleManager
            , IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult> CreateUserAsync(UserCreateInput input)
        {
            var user = User.CreateUser(input.UserName, input.Email, input.IsActive)
                            .AddToRoles(_roleManager.Roles.Where(r => input.Roles.Contains(r.Name)).ToArray());
            var identityResult = await _userManager.CreateAsync(user, input.Password);
            if (identityResult.Succeeded)
            {
                _logger.LogInformation($"A new account: {input.UserName} created with roles: {string.Join(",", input.Roles)}.");
                return ServiceResult.Success;
            }
            return ServiceResult.Fail(identityResult.Errors.Select(x => x.Description).ToArray());
        }

        public async Task<PagedListDto<UserDto>> GetUserPagedListAsync(string search, int skip, int take)
        {
            var count = await _userManager.Users
                                .WhereIf(!search.IsNullOrEmpty(), u => u.UserName.Contains(search) || u.Email.Contains(search))
                                .CountAsync();
            if (count > 0)
            {
                var list = _mapper.Map<IEnumerable<UserDto>>(await _userRepository.GetUsersIncludeRolesAsync(search, skip, take));
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
            return _mapper.Map<UserDto>(await _userRepository.FindUserIncludeRolesAsync(id));
        }

        public async Task<ServiceResult> EditUserAsync(UserEditInput input)
        {
            var user = await _userRepository.FindUserIncludeRolesAsync(input.Id);
            if (user == null)
                throw new UserFriendlyException("用户不存在");
            user.UpdateUserName(input.UserName)
                .UpdateEmail(input.Email)
                .UpdateState(input.IsActive)
                .AddToRoles(_roleManager.Roles.Where(r => input.Roles.Contains(r.Name)).ToArray());
            var identityResult = await _userManager.UpdateAsync(user);
            if (identityResult.Succeeded)
                return ServiceResult.Success;
            return ServiceResult.Fail(identityResult.Errors.Select(x => x.Description).ToArray());
        }
    }
}
