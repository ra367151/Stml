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
using Stml.Application.Dtos.Inputs;
using Stml.Application.Dtos.Outputs;
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
            var identityResult = await _userManager.CreateAsync(user.Enable(input.IsEnable), input.Password);
            if (identityResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, input.Roles.Where(u => u.Selected).Select(r => r.Name));
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

        public async Task<UserEditInput> FindUserEditModelAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new FriendlyException("用户不存在，可能已经被删除");
            var dto = _mapper.Map<UserEditInput>(user);
            var userRolesNames = await _userManager.GetRolesAsync(user);
            dto.Roles = await _roleManager.Roles
                                    .Select(x => new CheckboxRole(x.Id, x.Name, userRolesNames.Contains(x.Name)))
                                    .ToListAsync();
            return dto;
        }

        public async Task<ServiceResult> EditUserAsync(UserEditInput input)
        {
            var user = await _userManager.FindByIdAsync(input.Id.ToString());
            if (user == null)
                throw new FriendlyException("用户不存在，可能已经被删除");
            user = _mapper.Map<User>(input);
            var identityResult = await _userManager.UpdateAsync(user);
            if (identityResult.Succeeded)
            {
                return ServiceResult.Success;
            }
            return ServiceResult.Fail(identityResult.Errors.Select(x => x.Description).ToArray());
        }
    }
}
