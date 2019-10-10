using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Stml.Application.Dtos.Inputs;
using Stml.Application.Dtos.Outputs;
using Stml.Domain.Users;

namespace Stml.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountAppService(IMapper mapper
            , UserManager<User> userManager
            , SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserRegisterDto> UserRegisterAsync(UserRegisterInput input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user != null)
            {
                return new UserRegisterDto("用户名已存在");
            }
            user = new User { UserName = input.UserName, Email = input.Email };
            var identityResult = await _userManager.CreateAsync(user, input.Password);
            if (identityResult.Succeeded)
            {
                return new UserRegisterDto();
            }
            return new UserRegisterDto(identityResult.Errors.Select(x => x.Description));
        }

        public async Task<bool> UserLoginAsync(UserLoginInput input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null)
            {
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, input.Password, false, false);
            return result.Succeeded;
        }
    }
}
