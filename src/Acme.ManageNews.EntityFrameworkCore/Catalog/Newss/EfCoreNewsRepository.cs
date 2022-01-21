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

namespace Acme.ManageNews.Catalog.Newss
{
    public class EfCoreNewsRepository
        : EfCoreRepository<ManageNewsDbContext, News, Guid>,
            INewsRepository
    {
        public EfCoreNewsRepository(
            IDbContextProvider<ManageNewsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<News> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(x => x.Title == name);
        }

        public async Task<List<News>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    x => x.Title.Contains(filter)
                 )
                .OrderByDescending(x => x.DeletionTime)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
