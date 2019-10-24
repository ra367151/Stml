using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stml.Application.Roles.Dto;
using Stml.Domain.Roles;

namespace Stml.Application.Roles
{
    public class RoleAppService : IRoleAppService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleAppService(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            return await _roleManager.Roles.ProjectTo<RoleDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
