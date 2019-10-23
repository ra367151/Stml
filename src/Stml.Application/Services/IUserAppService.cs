using Stml.Application.Dtos.Inputs;
using Stml.Application.Dtos.Outputs;
using Stml.Infrastructure.Applications.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Application.Services
{
    public interface IUserAppService
    {
        Task<PagedListDto<UserDto>> GetUserPagedListAsync(string queryString, int skip, int take);
        Task<ServiceResult> CreateUserAsync(UserCreateInput input);
        Task DeleteUserAsync(Guid id);
        Task<UserEditInput> FindUserEditModelAsync(Guid id);
    }
}
