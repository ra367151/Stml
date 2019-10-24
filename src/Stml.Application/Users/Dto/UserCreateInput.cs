﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stml.Application.Users.Dto
{
    public class UserCreateInput
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// 角色名称列表
        /// </summary>
        public string[] Roles { get; set; }
    }
}