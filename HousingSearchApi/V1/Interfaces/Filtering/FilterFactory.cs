using Hackney.Shared.HousingSearch.Gateways.Models.Transactions;
using HousingSearchApi.V1.Boundary.Requests;
using Nest;
using System;

namespace HousingSearchApi.V1.Interfaces.Filtering
{
    public class FilterFactory : IFilterFactory
    {
        /// <summary>
        /// Method to perfom filtering from start to end dates
        /// </summary>
        public QueryContainer Filter<T, TRequest>(TRequest request, QueryContainerDescriptor<T> q) where T : class where TRequest : class
        {
            // ToDo: implement factory for choosing type of filter
            if (request is GetTransactionListRequest transactionSearchRequest &&
                q is QueryContainerDescriptor<QueryableTransaction> transactionDescriptor)
            {
                if (transactionSearchRequest.StartDate == DateTime.MinValue &&
                                                transactionSearchRequest.EndDate == DateTime.MinValue)
                    return null;

                if (transactionSearchRequest.StartDate == DateTime.MinValue)
                    transactionSearchRequest.StartDate = DateTime.UnixEpoch;

                if (transactionSearchRequest.EndDate == DateTime.MinValue)
                    transactionSearchRequest.EndDate = DateTime.UtcNow;

                return transactionDescriptor.DateRange(c => c
                    .Field(p => p.TransactionDate)
                    .GreaterThanOrEquals(transactionSearchRequest.StartDate)
                    .LessThanOrEquals(transactionSearchRequest.EndDate));
            }
            return null;
        }
    }
}
