using Acme.ManageNews.Catalog;
using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalog.Cities;
using Acme.ManageNews.Catalog.Eventss;
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
        //CreateMap<Category, AuthorLookupDto>();
        CreateMap<Topic, TopicDto>();
        //CreateMap<Category, AuthorLookupDto>();
        CreateMap<Events, EventsDto>();
        //CreateMap<Category, AuthorLookupDto>();
    }
}
