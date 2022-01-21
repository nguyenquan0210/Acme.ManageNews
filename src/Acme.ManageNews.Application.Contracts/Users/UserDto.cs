using System;
using Volo.Abp.Application.Dtos;

namespace Acme.ManageNews.Users
{
    public class UserDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}