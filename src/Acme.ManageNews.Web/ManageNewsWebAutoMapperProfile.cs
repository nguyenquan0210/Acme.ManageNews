using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalog.Cities;
using Acme.ManageNews.Catalog.Eventss;
using Acme.ManageNews.Catalog.Topics;
using AutoMapper;

namespace Acme.ManageNews.Web;

public class ManageNewsWebAutoMapperProfile : Profile
{
    public ManageNewsWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        // ADD a NEW MAPPING
        CreateMap<Pages.Categories.CreateModalModel.CreateCategoryViewModel,
                  CreateCategoryDto>();
        // ADD THESE NEW MAPPINGS
        CreateMap<CategoryDto, Pages.Categories.EditModalModel.EditCategoryViewModel>();
        CreateMap<Pages.Categories.EditModalModel.EditCategoryViewModel,UpdateCategoryDto>();

        CreateMap<Pages.Cities.CreateModalModel.CreateCityViewModel,
                  CreateCityDto>();
        // ADD THESE NEW MAPPINGS
        CreateMap<CityDto, Pages.Cities.EditModalModel.EditCityViewModel>();
        CreateMap<Pages.Cities.EditModalModel.EditCityViewModel, UpdateCityDto>();

        CreateMap<Pages.Topics.CreateModalModel.CreateTopicViewModel,
                  CreateTopicDto>();
        // ADD THESE NEW MAPPINGS
        CreateMap<TopicDto, Pages.Topics.EditModalModel.EditTopicViewModel>();
        CreateMap<Pages.Topics.EditModalModel.EditTopicViewModel, UpdateTopicDto>();

       
        CreateMap<Pages.Eventss.CreateModalModel.CreateEventsViewModel, CreateEventsDto>();
        CreateMap<EventsDto, Pages.Eventss.EditModalModel.EditEventsViewModel>();
        CreateMap<Pages.Eventss.EditModalModel.EditEventsViewModel, UpdateEventsDto>();
    }
}
