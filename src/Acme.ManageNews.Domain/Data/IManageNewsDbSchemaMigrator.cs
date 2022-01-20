using System.Threading.Tasks;

namespace Acme.ManageNews.Data;

public interface IManageNewsDbSchemaMigrator
{
    Task MigrateAsync();
}
