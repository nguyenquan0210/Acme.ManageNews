using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.ManageNews.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<UserDto> GetAsync(Guid id);

        Task<List<UserDto>> GetListAsync();

    }
}
