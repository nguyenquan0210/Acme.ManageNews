using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.ManageNews.Catalog.Newss
{
    public interface INewsAppService : IApplicationService
    {
        Task<NewsDto> GetAsync(Guid id);

        Task<PagedResultDto<NewsDto>> GetListAsync(GetCatalogListDto input);

        Task<NewsDto> CreateAsync(CreateNewsDto input);

        Task UpdateAsync(Guid id, UpdateNewsDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<CatalogLookupDto>> GetEventsLookupAsync();
        Task<ListResultDto<CatalogLookupDto>> GetCityLookupAsync();
        Task<ListResultDto<CatalogLookupDto>> GetTopicLookupAsync();
    }
}
