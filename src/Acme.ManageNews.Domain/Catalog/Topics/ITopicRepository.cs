using Acme.ManageNews.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.ManageNews.Catalog.Topics
{
    public interface ITopicRepository : IRepository<Topic, Guid>
    {
        Task<Topic> FindByNameAsync(string name);

        Task<List<Topic>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}