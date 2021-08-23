using System.Threading.Tasks;
using Hackney.Core.Logging;
using HousingSearchApi.V1.Boundary.Requests;
using HousingSearchApi.V1.Boundary.Response;
using HousingSearchApi.V1.Interfaces;
using HousingSearchApi.V1.UseCase.Interfaces;

namespace HousingSearchApi.V1.UseCase
{
    public class GetTenureListUseCase : IGetTenureListUseCase
    {
        private readonly ISearchGateway _searchGateway;

        public GetTenureListUseCase(ISearchGateway searchGateway)
        {
            _searchGateway = searchGateway;
        }

        [LogCall]
        public async Task<GetTenureListResponse> ExecuteAsync(HousingSearchRequest housingSearchRequest)
        {
            return await _searchGateway.GetListOfTenures(housingSearchRequest).ConfigureAwait(false);
        }
    }
}
