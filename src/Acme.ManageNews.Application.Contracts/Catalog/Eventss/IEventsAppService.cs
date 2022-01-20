using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.ManageNews.Catalog.Eventss
{
    public interface IEventsAppService : IApplicationService
    {
        Task<EventsDto> GetAsync(Guid id);

        Task<PagedResultDto<EventsDto>> GetListAsync(GetCatalogListDto input);

        Task<EventsDto> CreateAsync(CreateEventsDto input);

        Task UpdateAsync(Guid id, UpdateEventsDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<CatalogLookupDto>> GetCategoryLookupAsync();
    }
}
