using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Acme.ManageNews.Catalog.Categories
{
    public class CategoryAlreadyExistsException : BusinessException
    {
        public CategoryAlreadyExistsException(string name)
            : base(ManageNewsDomainErrorCodes.AlreadyExists)
        {
            WithData("name", name);
        }
    }
}
