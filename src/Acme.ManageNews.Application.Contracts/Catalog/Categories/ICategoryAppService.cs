using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.ManageNews.Catalog.Categories
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<CategoryDto> GetAsync(Guid id);

        Task<PagedResultDto<CategoryDto>> GetListAsync(GetCatalogListDto input);

        Task<CategoryDto> CreateAsync(CreateCategoryDto input);

        Task UpdateAsync(Guid id, UpdateCategoryDto input);

        Task DeleteAsync(Guid id);
    }
}
