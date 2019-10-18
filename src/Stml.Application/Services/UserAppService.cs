using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stml.Application.Dtos.Outputs;
using Stml.Domain.Users;
using Stml.Infrastructure.Applications.Dto;
using Stml.Infrastructure.Collection.Extensions;
using Stml.Infrastructure.Extensions;
using Stml.Infrastructure.Linq.Extensions;

namespace Stml.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;

        public UserAppService(UserManager<User> userManager
            , IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PagedListDto<UserDto>> GetUserPagedListAsync(string queryString)
        {
            var count = await _userManager.Users
                                .WhereIf(!queryString.IsNullOrEmpty(), u => u.UserName == queryString.Trim())
                                .CountAsync();
            if (count > 0)
            {
                var list = await _userManager.Users
                                    .WhereIf(!queryString.IsNullOrEmpty(), u => u.UserName == queryString.Trim())
                                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
                return new PagedListDto<UserDto>(count, list);
            }
            return PagedListDto<UserDto>.Null;
        }
    }
}
