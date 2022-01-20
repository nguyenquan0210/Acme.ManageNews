using Acme.ManageNews.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.ManageNews.Catalog.Eventss
{
    public interface IEventsRepository : IRepository<Events, Guid>
    {
        Task<Events> FindByNameAsync(string name);

        Task<List<Events>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}