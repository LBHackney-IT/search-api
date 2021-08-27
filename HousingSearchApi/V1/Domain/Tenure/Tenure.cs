using System.Collections.Generic;
using System.Linq;
using HousingSearchApi.V1.Gateways.Models;
using HousingSearchApi.V1.Helper;

namespace HousingSearchApi.V1.Domain.Tenure
{
    public class Tenure
    {
        public static Tenure Create(string id, string paymentReference, string startOfTenureDate, string endOfTenureDate,
            QueryableTenuredAsset tenuredAsset, string tenureType)
        {
            return new Tenure(id, paymentReference, startOfTenureDate, endOfTenureDate,
                tenuredAsset, tenureType);
        }

        public Tenure()
        {

        }

        private Tenure(string id, string paymentReference, string startOfTenureDate, string endOfTenureDate,
            QueryableTenuredAsset tenuredAsset, string tenureType)
        {
            Id = id;
            PaymentReference = paymentReference;
            StartOfTenureDate = startOfTenureDate;
            EndOfTenureDate = endOfTenureDate;
            TenuredAsset = TenuredAsset.Create(tenuredAsset);
            Type = tenureType;
        }

        public string Id { get; set; }
        public string PaymentReference { get; set; }
        public List<HouseholdMember> HouseholdMembers { get; set; }
        public TenuredAsset TenuredAsset { get; set; }
        public string StartOfTenureDate { get; set; }
        public string EndOfTenureDate { get; set; }
        public string Type { get; set; }
        public bool IsActive => TenureHelpers.IsTenureActive(EndOfTenureDate);
    }
}
