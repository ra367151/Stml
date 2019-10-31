using AutoMapper;
using Stml.Domain.Authorizations;
using Stml.Infrastructure.Authorizations.Permissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Permission[] Permissions { get; set; }
    }
}
