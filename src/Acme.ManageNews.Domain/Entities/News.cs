using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.ManageNews.Entities
{
    public class News : FullAuditedAggregateRoot<Guid>
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
       
        public Guid UserId { get; set; }

        public Guid CityId { get; set; }
        
        public Guid TopicId { get; set; }
       
    }
}
