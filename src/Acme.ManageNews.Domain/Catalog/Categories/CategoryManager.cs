using Acme.ManageNews.Entities;
using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.ManageNews.Catalog.Categories
{
    public class CategoryManager : DomainService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateAsync(
             [NotNull] string name,
            Status Status,
            int SortOrder)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingCategory = await _categoryRepository.FindByNameAsync(name);
            if (existingCategory != null)
            {
                throw new CategoryAlreadyExistsException(name);
            }

            return new Category(
                GuidGenerator.Create(),
                name,
                Status,
                SortOrder
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Category Category,
            [NotNull] string newName)
        {
            Check.NotNull(Category, nameof(Category));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingCategory = await _categoryRepository.FindByNameAsync(newName);
            if (existingCategory != null && existingCategory.Id != Category.Id)
            {
                throw new CategoryAlreadyExistsException(newName);
            }

            Category.ChangeName(newName);
        }
    }
}
