using HousingSearchApi.V1.Boundary.Requests;
using HousingSearchApi.V1.Domain;
using HousingSearchApi.V1.Domain.ES;
using Nest;

namespace HousingSearchApi.V1.Interfaces
{
    public interface ISearchPersonQueryContainer
    {
        QueryContainer Create(GetPersonListRequest request,
            QueryContainerDescriptor<QueryablePerson> q);
    }
}
