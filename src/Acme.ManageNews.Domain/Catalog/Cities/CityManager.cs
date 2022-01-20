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

namespace Acme.ManageNews.Catalog.Cities
{
    public class CityManager : DomainService
    {
        private readonly ICityRepository _cityRepository;

        public CityManager(ICityRepository  cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<City> CreateAsync(
             [NotNull] string name,
            Status Status,
            int SortOrder)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingCity = await _cityRepository.FindByNameAsync(name);
            if (existingCity != null)
            {
                throw new CityAlreadyExistsException(name);
            }

            return new City(
                GuidGenerator.Create(),
                name,
                Status,
                SortOrder
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] City City,
            [NotNull] string newName)
        {
            Check.NotNull(City, nameof(City));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingCity = await _cityRepository.FindByNameAsync(newName);
            if (existingCity != null && existingCity.Id != City.Id)
            {
                throw new CityAlreadyExistsException(newName);
            }

            City.ChangeName(newName);
        }
    }
}
