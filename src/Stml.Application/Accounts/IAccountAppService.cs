using Stml.Application.Accounts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Application.Accounts
{
    public interface IAccountAppService
    {
        Task<bool> UserLoginAsync(UserLoginInput input);
        Task UserLogoutAsync();
    }
}
