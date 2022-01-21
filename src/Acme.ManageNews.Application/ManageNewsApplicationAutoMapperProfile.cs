using Acme.ManageNews.Catalog;
using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalog.Cities;
using Acme.ManageNews.Catalog.Eventss;
using Acme.ManageNews.Catalog.Newss;
using Acme.ManageNews.Catalog.Topics;
using Acme.ManageNews.Entities;
using AutoMapper;

namespace Acme.ManageNews;

public class ManageNewsApplicationAutoMapperProfile : Profile
{
    public ManageNewsApplicationAutoMapperProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CatalogLookupDto>();
        CreateMap<City, CityDto>();
        CreateMap<City, CatalogLookupDto>();
        CreateMap<Topic, TopicDto>();
        CreateMap<Topic, CatalogLookupDto>();
        CreateMap<Events, EventsDto>();
        CreateMap<Events, CatalogLookupDto>();

        CreateMap<News, NewsDto>();
    }
}
