using AutoMapper;
using Stml.Application.Roles;
using Stml.Application.Roles.Dto;
using Stml.Application.Users.Dto;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Web.Models.Roles;
using Stml.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web
{
    public class WebMapProfile : Profile
    {
        public WebMapProfile()
        {
            #region role mappers
            CreateMap<RoleCreateViewModel, RoleCreateInput>()
                .ForMember(dest => dest.Permissions,
                    opt => opt.MapFrom(src => src.Permissions.Where(p => p.Checked).Select(p => p.Name).ToArray()));

            CreateMap<RoleDto, RoleEditViewModel>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom<RolePermissionsValueResolver>());

            CreateMap<RoleEditViewModel, RoleEditInput>()
                .ForMember(dest => dest.Permissions,
                    opt => opt.MapFrom(src => src.Permissions.Where(p => p.Checked).Select(p => p.Name).ToArray()));
            #endregion

            #region user mappers
            CreateMap<UserCreateViewModel, UserCreateInput>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src => src.Roles.Where(r => r.Checked).Select(r => r.Name).ToArray()));

            CreateMap<UserDto, UserEditViewModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom<UserRolesValueResolver>());

            CreateMap<UserEditViewModel, UserEditInput>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src => src.Roles.Where(r => r.Checked).Select(r => r.Name).ToArray()));
            #endregion
        }
    }

    #region RolePermissionsValueResolver
    public class RolePermissionsValueResolver : IValueResolver<RoleDto, RoleEditViewModel, List<PermissionCheckboxViewModel>>
    {
        private readonly IPermissionManager<Permission> _permissionManager;

        public RolePermissionsValueResolver(IPermissionManager<Permission> permissionManager)
        {
            _permissionManager = permissionManager;
        }

        public List<PermissionCheckboxViewModel> Resolve(RoleDto source, RoleEditViewModel destination, List<PermissionCheckboxViewModel> destMember, ResolutionContext context)
        {
            return _permissionManager.Permissions
                                    .SelectMany(x => x.Value)
                                    .OrderBy(x => x.Group)
                                    .Select(x => new PermissionCheckboxViewModel(
                                        x.Group,
                                        x.Name,
                                        x.DisplayName,
                                        source.Permissions.Any(p => p.Name == x.Name))
                                    ).ToList();
        }
    }
    #endregion

    #region UserRolesValueResolver
    public class UserRolesValueResolver : IValueResolver<UserDto, UserEditViewModel, List<RoleCheckboxViewModel>>
    {
        private readonly IRoleAppService _roleAppService;

        public UserRolesValueResolver(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        public List<RoleCheckboxViewModel> Resolve(UserDto source, UserEditViewModel destination, List<RoleCheckboxViewModel> destMember, ResolutionContext context)
        {
            var roles = _roleAppService.GetRolesAsync().Result;
            return roles.Select(x => new RoleCheckboxViewModel(x.Name, x.DisplayName, source.Roles.Any(r => r.Name == x.Name))).ToList();
        }
    }
    #endregion
}
