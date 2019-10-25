using Stml.Application.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Users.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime CreationTime { get; set; }

        public bool IsActive { get; set; }

        public string DateOfCreation
        {
            get
            {
                return CreationTime.ToShortDateString();
            }
        }

        public string TimeOfCreation
        {
            get
            {
                return CreationTime.ToLongTimeString();
            }
        }

        public RoleDto[] Roles { get; set; }
    }
}
