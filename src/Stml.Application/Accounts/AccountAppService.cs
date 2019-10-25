using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Stml.Application.Accounts.Dto;
using Stml.Domain.Authorizations;

namespace Stml.Application.Accounts
{
    public class AccountAppService : IAccountAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountAppService> _logger;

        public AccountAppService(ILogger<AccountAppService> logger
            , UserManager<User> userManager
            , SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> UserLoginAsync(UserLoginInput input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null || !user.IsActive)
            {
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, false);
            return result.Succeeded;
        }

        public async Task UserLogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
