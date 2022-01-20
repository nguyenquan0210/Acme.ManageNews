using Acme.ManageNews.Catalog.Categories;
using Acme.ManageNews.Catalog.Cities;
using Acme.ManageNews.Catalog.Eventss;
using Acme.ManageNews.Catalog.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.ManageNews
{
    public class ManageNewsDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryManager _categoryManager;
        private readonly ICityRepository _cityRepository;
        private readonly CityManager _cityManager;
        private readonly ITopicRepository _topicRepository;
        private readonly TopicManager _topicManager;
        private readonly IEventsRepository _eventsRepository;
        private readonly EventsManager _eventsManager;

        public ManageNewsDataSeederContributor(
            ICategoryRepository categoryRepository,
            CategoryManager categoryManager,
            ICityRepository cityRepository, CityManager cityManager,
            ITopicRepository topicRepository, TopicManager topicManager,
            IEventsRepository eventsRepository, EventsManager eventsManager)
        {
            _categoryRepository = categoryRepository;
            _categoryManager = categoryManager;
            _cityManager = cityManager;
            _cityRepository = cityRepository;
            _topicManager = topicManager;
            _topicRepository = topicRepository;
            _eventsManager = eventsManager;
            _eventsRepository = eventsRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _topicRepository.GetCountAsync() == 0)
            {
                var topic1 = await _topicRepository.InsertAsync(
                await _topicManager.CreateAsync(
                    "Món ngon mỗi ngày",
                    Enums.Status.Active,
                    1,
                    true
                    )
                );
                var topic2 = await _topicRepository.InsertAsync(
                    await _topicManager.CreateAsync(
                        "Thời tiết hôm nay",
                        Enums.Status.Active,
                        1,
                        true
                    )
                );
            }
            if (await _cityRepository.GetCountAsync() == 0)
            {
                var city1 = await _cityRepository.InsertAsync(
                await _cityManager.CreateAsync(
                    "Hà Nội",
                    Enums.Status.Active,
                    1
                    )
                );
                var city2 = await _cityRepository.InsertAsync(
                    await _cityManager.CreateAsync(
                        "Đà Nẵng",
                        Enums.Status.Active,
                        1
                    )
                );
            }
            
               
                
           
            if (await _eventsRepository.GetCountAsync() > 0)
            {
                return;
            }
            var category1 = await _categoryRepository.InsertAsync(
               await _categoryManager.CreateAsync(
                   "Xã Hội",
                   Enums.Status.Active,
                   1
               )
           );
            var category2 = await _categoryRepository.InsertAsync(
                await _categoryManager.CreateAsync(
                    "Pháp Luật",
                    Enums.Status.Active,
                    1
                )
            );
            var event1 = await _eventsRepository.InsertAsync(
                await _eventsManager.CreateAsync(
                    "Tiêm vaccine Covid-19 cho trẻ em",
                    Enums.Status.Active,
                    1,
                    false,
                    category1.Id
                    )
                );
            var event2 = await _eventsRepository.InsertAsync(
                await _eventsManager.CreateAsync(
                    "Loạt ca nhiễm nCoV mới ở Việt Nam",
                    Enums.Status.Active,
                    1,
                    false,
                    category2.Id
                )
            );




        }
    }
}
