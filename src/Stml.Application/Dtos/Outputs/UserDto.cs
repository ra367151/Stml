using AutoMapper;
using Stml.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Dtos.Outputs
{
    [AutoMap(typeof(User))]
    public class UserDto
    {
        public string UserName { get; set; }
    }
}
