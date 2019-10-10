using Stml.Application.Dtos.Inputs;
using Stml.Application.Dtos.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Application.Services
{
    public interface IAccountAppService
    {
        /// <summary>
        /// 注册用户账户
        /// </summary>
        /// <param name="input">用户注册信息实体</param>
        /// <returns>返回注册结果</returns>
        Task<UserRegisterDto> UserRegisterAsync(UserRegisterInput input);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回登录结果</returns>
        Task<bool> UserLoginAsync(UserLoginInput input);
    }
}
