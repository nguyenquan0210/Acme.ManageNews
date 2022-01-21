using Acme.ManageNews.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.ManageNews.Catalog.Newss
{
    public interface INewsRepository : IRepository<News, Guid>
    {
        Task<News> FindByNameAsync(string name);

        Task<List<News>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}