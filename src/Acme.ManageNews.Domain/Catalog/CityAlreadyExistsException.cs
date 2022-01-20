using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Acme.ManageNews.Catalog
{
    [Serializable]
    public class CatalogAlreadyExistsException : BusinessException
    {
        public CatalogAlreadyExistsException(string name)
            : base(ManageNewsDomainErrorCodes.AlreadyExists)
        {
            WithData("name", name);
        }
    }
}