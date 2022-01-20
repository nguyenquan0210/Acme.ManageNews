using Acme.ManageNews.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.ManageNews.Catalog.Cities
{
    public interface ICityRepository : IRepository<City, Guid>
    {
        Task<City> FindByNameAsync(string name);

        Task<List<City>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
