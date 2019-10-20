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
        Task<PagedListDto<UserDto>> GetUserPagedListAsync(string queryString, int pageNumber, int pageSize);
    }
}
