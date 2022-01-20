using Volo.Abp.Application.Dtos;

namespace Acme.ManageNews.Catalog.Cities
{
    public class GetCityListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}