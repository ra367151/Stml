using AutoMapper;
using Stml.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Dtos.Outputs
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CreationTime { get; set; }
        public bool IsEnable { get; set; }
    }
}
