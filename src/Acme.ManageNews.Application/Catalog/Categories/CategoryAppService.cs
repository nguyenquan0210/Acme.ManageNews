using Acme.ManageNews.Entities;
using Acme.ManageNews.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.ManageNews.Catalog.Categories
{
    [Authorize(ManageNewsPermissions.Categories.Default)]
    public class CategoryAppService : ManageNewsAppService, ICategoryAppService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryManager _categoryManager;

        public CategoryAppService(
            ICategoryRepository categoryRepository,
            CategoryManager categoryManager)
        {
            _categoryRepository = categoryRepository;
            _categoryManager = categoryManager;
        }

        [Authorize(ManageNewsPermissions.Categories.Create)]
        public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
        {
            var category = await _categoryManager.CreateAsync(
                input.Name,
                input.Status = Enums.Status.Active,
                input.SortOrder
            );

            await _categoryRepository.InsertAsync(category);

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        [Authorize(ManageNewsPermissions.Categories.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<CategoryDto> GetAsync(Guid id)
        {
            var category = await _categoryRepository.GetAsync(id);
            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCatalogListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Category.Name);
            }

            var categorys = await _categoryRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _categoryRepository.CountAsync()
                : await _categoryRepository.CountAsync(
                    category => category.Name.Contains(input.Filter));

            return new PagedResultDto<CategoryDto>(
                totalCount,
                ObjectMapper.Map<List<Category>, List<CategoryDto>>(categorys)
            );
        }

        [Authorize(ManageNewsPermissions.Categories.Edit)]
        public async Task UpdateAsync(Guid id, UpdateCategoryDto input)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category.Name != input.Name)
            {
                await _categoryManager.ChangeNameAsync(category, input.Name);
            }

            category.SortOrder = input.SortOrder;
            category.Status = input.Status;

            await _categoryRepository.UpdateAsync(category);
        }

        //...SERVICE METHODS WILL COME HERE...
    }
}
