using AutoMapper;
using Stml.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Users.Dto
{
    [AutoMap(typeof(User))]
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

        public string[] Roles { get; set; }
    }
}
