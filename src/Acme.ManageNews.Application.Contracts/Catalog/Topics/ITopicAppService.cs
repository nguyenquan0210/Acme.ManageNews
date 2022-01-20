using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.ManageNews.Catalog.Topics
{
    public interface ITopicAppService : IApplicationService
    {
        Task<TopicDto> GetAsync(Guid id);

        Task<PagedResultDto<TopicDto>> GetListAsync(GetCatalogListDto input);

        Task<TopicDto> CreateAsync(CreateTopicDto input);

        Task UpdateAsync(Guid id, UpdateTopicDto input);

        Task DeleteAsync(Guid id);
    }
}
