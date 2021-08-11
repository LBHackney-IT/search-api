using Nest;

namespace HousingSearchApi.V1.Gateways.Models
{
    public class QueryablePersonProperty
    {
        [Text(Name = "id")]
        public string Id { get; set; }

        [Text(Name = "type")]
        public string Type { get; set; }

        [Text(Name = "totalBalance")]
        public decimal TotalBalance { get; set; }

        [Text(Name = "assetFullAddress")]
        public string AssetFullAddress { get; set; }

        [Text(Name = "postCode")]
        public string PostCode { get; set; }

        [Text(Name = "rentAccountNumber")]
        public string RentAccountNumber { get; set; }
    }
}