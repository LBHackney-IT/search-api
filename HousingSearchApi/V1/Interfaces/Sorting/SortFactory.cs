using Hackney.Shared.HousingSearch.Gateways.Models.Assets;
using Hackney.Shared.HousingSearch.Gateways.Models.Persons;
using HousingSearchApi.V1.Boundary.Requests;

namespace HousingSearchApi.V1.Interfaces.Sorting
{
    public class SortFactory : ISortFactory
    {
        public ISort<T> Create<T>(HousingSearchRequest request) where T : class
        {
            if (typeof(T) == typeof(QueryablePerson))
            {
                if (string.IsNullOrEmpty(request.SortBy))
                    return new DefaultSort<T>();

                switch (request.IsDesc)
                {
                    case true:
                        return (ISort<T>) new SurnameDesc();
                    case false:
                        return (ISort<T>) new SurnameAsc();
                }
            }
            if (typeof(T) == typeof(QueryableAsset))
            {
                if (string.IsNullOrEmpty(request.SortBy))
                    return new DefaultSort<T>();

                switch (request.IsDesc)
                {
                    case true:
                        return (ISort<T>) new AssetIdDesc();
                    case false:
                        return (ISort<T>) new AssetIdAsc();
                }
            }

            return new DefaultSort<T>();
        }
    }
}
