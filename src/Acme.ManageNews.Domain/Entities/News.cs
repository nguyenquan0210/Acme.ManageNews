using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
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

        private News()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal News(
            Guid id,
            [NotNull] string title,
            [NotNull] string description,
            [NotNull] string content,
            Status status,
            Status newsHot,
            [NotNull] string img,
            int viewss,
            [NotNull] string keyword,
            Guid cityId,
            Guid topicId,
            Guid eventId,
            Guid userId,
            [JetBrains.Annotations.CanBeNull] string url = null,
            [JetBrains.Annotations.CanBeNull] string video = null)
            : base(id)
        {
            Title = title;
            Description = description;
            Content = content;
            Status = status;
            NewsHot = newsHot;
            Img = img;
            Keyword = keyword;
            Viewss = viewss;
            CityId = cityId;
            TopicId = topicId;
            EventId = eventId;
            UserId = userId;
            Url = url;
            Video = video;
        }

        

        

    }
}
