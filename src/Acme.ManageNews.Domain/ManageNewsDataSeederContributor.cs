using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalog.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.ManageNews
{
    public class ManageNewsDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryManager _categoryManager;
        private readonly ICityRepository _cityRepository;
        private readonly CityManager _cityManager;


        public ManageNewsDataSeederContributor(
            ICategoryRepository categoryRepository,
            CategoryManager categoryManager,
            ICityRepository cityRepository, CityManager cityManager)
        {
            _categoryRepository = categoryRepository;
            _categoryManager = categoryManager;
            _cityManager = cityManager;
            _cityRepository = cityRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _categoryRepository.GetCountAsync() == 0)
            {
                var category1 = await _categoryRepository.InsertAsync(
                await _categoryManager.CreateAsync(
                    "Xã Hội",
                    Enums.Status.Active,
                    1
                )
            );
                var category2 = await _categoryRepository.InsertAsync(
                    await _categoryManager.CreateAsync(
                        "Pháp Luật",
                        Enums.Status.Active,
                        1
                    )
                );
            }
            if (await _cityRepository.GetCountAsync() == 0)
            {
                var city1 = await _cityRepository.InsertAsync(
                await _cityManager.CreateAsync(
                    "Hà Nội",
                    Enums.Status.Active,
                    1
                    )
                );
                var city2 = await _cityRepository.InsertAsync(
                    await _cityManager.CreateAsync(
                        "Đà Nẵng",
                        Enums.Status.Active,
                        1
                    )
                );
            }

            

            



        }
    }
}
