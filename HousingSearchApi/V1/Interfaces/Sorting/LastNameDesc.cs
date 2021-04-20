using HousingSearchApi.V1.Domain;
using HousingSearchApi.V1.Domain.ES;
using Nest;

namespace HousingSearchApi.V1.Interfaces.Sorting
{
    public class SurnameDesc : IPersonListSort
    {
        public SortDescriptor<QueryablePerson> Get(SortDescriptor<QueryablePerson> descriptor)
        {
            return descriptor
                .Descending(f => f.Surname)
                .Descending(f => f.Firstname);
        }
    }
}
