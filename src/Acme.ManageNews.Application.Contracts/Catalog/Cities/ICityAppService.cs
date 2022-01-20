using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.ManageNews.Catalog.Cities
{
    public interface ICityAppService : IApplicationService
    {
        Task<CityDto> GetAsync(Guid id);

        Task<PagedResultDto<CityDto>> GetListAsync(GetCatalogListDto input);

        Task<CityDto> CreateAsync(CreateCityDto input);

        Task UpdateAsync(Guid id, UpdateCityDto input);

        Task DeleteAsync(Guid id);
    }
}
