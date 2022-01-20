using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalog.Cities;
using Acme.ManageNews.Entities;
using AutoMapper;

namespace Acme.ManageNews;

public class ManageNewsApplicationAutoMapperProfile : Profile
{
    public ManageNewsApplicationAutoMapperProfile()
    {
        CreateMap<Category, CategoryDto>();
        //CreateMap<Category, AuthorLookupDto>();
        CreateMap<City, CityDto>();
        //CreateMap<Category, AuthorLookupDto>();
    }
}
