using Acme.ManageNews.Enums;
using System;
using Volo.Abp.Application.Dtos;

namespace Acme.ManageNews.Catalog.Newss
{
    public class NewsDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }

        public string Img { get; set; }

        public string Video { get; set; }

        public int Viewss { get; set; }

        public Status NewsHot { get; set; }

        public Status Status { get; set; }

        public string Keyword { get; set; }

        public Guid EventId { get; set; }

        public string EventsName { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public Guid CityId { get; set; }
        public string CityName { get; set; }

        public Guid TopicId { get; set; }
        public string TopicName { get; set; }

        public Guid CategotyId { get; set; }
        public string CategoryName { get; set; }
    }
}