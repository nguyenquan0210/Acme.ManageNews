using Acme.ManageNews.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.ManageNews.Catalog.Newss
{
    public class CreateNewsDto
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public string Content { get; set; }
        [Required]
        [StringLength(100)]
        public string Img { get; set; }
        [Required]
        public Status NewsHot { get; set; }
        public Status Status { get; set; }
        [Required]
        [StringLength(255)]
        public string Keyword { get; set; }

        public Guid EventId { get; set; }

        public Guid UserId { get; set; }

        public Guid CityId { get; set; }

        public Guid TopicId { get; set; }

        public string Video { get; set; }

        public string Url { get; set; }
        public int Viewss { get; set; }
    }
}