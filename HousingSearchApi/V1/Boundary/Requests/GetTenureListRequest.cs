using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HousingSearchApi.V1.Boundary.Requests
{
    public class GetTenureListRequest
    {
        public GetTenureListRequest()
        {
            PageSize = Startup.StaticConfiguration.GetValue<int>("DefaultPageSize");
        }

        [FromQuery(Name = "searchText")]
        public string SearchText { get; set; }

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }

        [FromQuery(Name = "page")]
        public int Page { get; set; }

        [FromQuery(Name = "sortBy")]
        public string SortBy { get; set; }

        [FromQuery(Name = "isDesc")]
        public bool IsDesc { get; set; }
    }
}