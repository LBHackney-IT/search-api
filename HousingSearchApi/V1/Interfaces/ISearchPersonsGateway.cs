// TODO: 1 Return when last commit
//using Hackney.Core.Logging;
using HousingSearchApi.V1.Boundary.Requests;
using HousingSearchApi.V1.Boundary.Response;
using System.Linq;
using System.Threading.Tasks;

namespace HousingSearchApi.V1.Interfaces
{
    public interface ISearchPersonsGateway
    {
        Task<GetPersonListResponse> GetListOfPersons(GetPersonListRequest query);
    }

    public class SearchPersonsGateway : ISearchPersonsGateway
    {
        private readonly ISearchPersonElasticSearchHelper _elasticSearchHelper;

        public SearchPersonsGateway(ISearchPersonElasticSearchHelper elasticSearchHelper)
        {
            _elasticSearchHelper = elasticSearchHelper;
        }
        // TODO: 1 Return when last commit
        //[LogCall]
        public async Task<GetPersonListResponse> GetListOfPersons(GetPersonListRequest query)
        {
            var searchResponse = await _elasticSearchHelper.Search(query).ConfigureAwait(false);
            var personListResponse = new GetPersonListResponse();

            personListResponse.Persons.AddRange(searchResponse.Documents.Select(queryablePerson =>
                queryablePerson.Create())
            );

            personListResponse.SetTotal(searchResponse.Total);

            return personListResponse;
        }
    }
}
