using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Acme.ManageNews.Catalog.Cities
{
    [Serializable]
    public class CityAlreadyExistsException : BusinessException
    {
        public CityAlreadyExistsException(string name)
            : base(ManageNewsDomainErrorCodes.AlreadyExists)
        {
            WithData("name", name);
        }
    }
}