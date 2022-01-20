using Acme.ManageNews.Entities;
using Acme.ManageNews.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.ManageNews.Catalog.Cities
{
    [Authorize(ManageNewsPermissions.Cities.Default)]
    public class CityAppService : ManageNewsAppService, ICityAppService
    {
        private readonly ICityRepository _cityRepository;
        private readonly CityManager _cityManager;

        public CityAppService(
            ICityRepository cityRepository,
            CityManager cityManager)
        {
            _cityRepository = cityRepository;
            _cityManager = cityManager;
        }

        [Authorize(ManageNewsPermissions.Categories.Create)]
        public async Task<CityDto> CreateAsync(CreateCityDto input)
        {
            var city = await _cityManager.CreateAsync(
                input.Name,
                input.Status = Enums.Status.Active,
                input.SortOrder
            );

            await _cityRepository.InsertAsync(city);

            return ObjectMapper.Map<City, CityDto>(city);
        }

        [Authorize(ManageNewsPermissions.Categories.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _cityRepository.DeleteAsync(id);
        }

        public async Task<CityDto> GetAsync(Guid id)
        {
            var city = await _cityRepository.GetAsync(id);
            return ObjectMapper.Map<City, CityDto>(city);
        }

        public async Task<PagedResultDto<CityDto>> GetListAsync(GetCatalogListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(City.Name);
            }

            var citys = await _cityRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _cityRepository.CountAsync()
                : await _cityRepository.CountAsync(
                    x => x.Name.Contains(input.Filter));

            return new PagedResultDto<CityDto>(
                totalCount,
                ObjectMapper.Map<List<City>, List<CityDto>>(citys)
            );
        }

        [Authorize(ManageNewsPermissions.Categories.Edit)]
        public async Task UpdateAsync(Guid id, UpdateCityDto input)
        {
            var city = await _cityRepository.GetAsync(id);

            if (city.Name != input.Name)
            {
                await _cityManager.ChangeNameAsync(city, input.Name);
            }

            city.SortOrder = input.SortOrder;
            city.Status = input.Status;

            await _cityRepository.UpdateAsync(city);
        }

        //...SERVICE METHODS WILL COME HERE...
    }
}
