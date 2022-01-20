using Acme.ManageNews.Entities;
using Acme.ManageNews.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.ManageNews.Catalog.Eventss
{
    public class EfCoreEventsRepository
        : EfCoreRepository<ManageNewsDbContext, Events, Guid>,
            IEventsRepository
    {
        public EfCoreEventsRepository(
            IDbContextProvider<ManageNewsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Events> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Events>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    x => x.Name.Contains(filter)
                 )
                .OrderBy(x => x.SortOrder)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
