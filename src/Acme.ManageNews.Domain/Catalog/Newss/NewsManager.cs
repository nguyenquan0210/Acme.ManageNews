using Acme.ManageNews.Entities;
using Acme.ManageNews.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.ManageNews.Catalog.Newss
{
    public class NewsManager : DomainService
    {
        private readonly INewsRepository _newsRepository;

        public NewsManager(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public News CreateAsync(
        [NotNull]string Title,
        [NotNull] string Description,
        [NotNull] string Content,
        Status Status,
        Status NewsHot,
        [NotNull] string Img,
        int Viewss,
        [NotNull] string Keyword,
        Guid CityId,
        Guid TopicId,
        Guid EventId,
        Guid UserId,
        [JetBrains.Annotations.CanBeNull] string Url = null,
        [JetBrains.Annotations.CanBeNull] string Video = null)
        {
            return new News(
                GuidGenerator.Create(),
                Title,
                Description,
                Content,
                Status,
                NewsHot,
                Img,
                Viewss,
                Keyword, CityId, TopicId, EventId, UserId, Url,Video
            );
        }

        
    }
}
