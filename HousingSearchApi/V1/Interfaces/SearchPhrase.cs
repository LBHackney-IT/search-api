using HousingSearchApi.V1.Boundary.Requests;
using HousingSearchApi.V1.Gateways.Models;
using HousingSearchApi.V1.Infrastructure;
using Nest;

namespace HousingSearchApi.V1.Interfaces
{
    public class SearchPhrase : ISearchPersonQueryContainer
    {
        private readonly IWildCardAppenderAndPrepender _wildCardAppenderAndPrepender;

        public SearchPhrase(IWildCardAppenderAndPrepender wildCardAppenderAndPrepender)
        {
            _wildCardAppenderAndPrepender = wildCardAppenderAndPrepender;
        }

        public QueryContainer Create(GetPersonListRequest request, QueryContainerDescriptor<QueryablePerson> q)
        {
            if (string.IsNullOrWhiteSpace(request.SearchText)) return null;
            var searchText = request.SearchText?.Replace(" ", "").ToLower();

            var listOfWildCardedWords = _wildCardAppenderAndPrepender.Process(request.SearchText);

            var searchSurnames = q.QueryString(m =>
                m.Query(string.Join(' ', listOfWildCardedWords))
                    .Fields(f => f.Field(p => p.Firstname).Field(p => p.Surname))
                    .Type(TextQueryType.MostFields));

            return searchSurnames;
        }
    }
}
