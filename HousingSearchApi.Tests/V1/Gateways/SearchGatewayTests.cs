using AutoFixture;
using FluentAssertions;
using Hackney.Shared.HousingSearch.Gateways.Models.Assets;
using Hackney.Shared.HousingSearch.Gateways.Models.Persons;
using HousingSearchApi.V1.Boundary.Requests;
using HousingSearchApi.V1.Boundary.Responses;
using HousingSearchApi.V1.Factories;
using HousingSearchApi.V1.Gateways;
using HousingSearchApi.V1.Gateways.Interfaces;
using HousingSearchApi.V1.Helper.Interfaces;
using HousingSearchApi.V1.Interfaces;
using Moq;
using Nest;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HousingSearchApi.Tests.V1.Gateways
{
    public class SearchGatewayTests
    {
        private readonly SearchGateway _searchGateway;
        private readonly Mock<IElasticSearchWrapper> _elasticSearchWrapperMock;
        private readonly Mock<ICustomAddressSorter> _customAddressSorterMock;

        private readonly Fixture _fixture = new Fixture();

        public SearchGatewayTests()
        {
            _elasticSearchWrapperMock = new Mock<IElasticSearchWrapper>();
            _customAddressSorterMock = new Mock<ICustomAddressSorter>();

            _searchGateway = new SearchGateway(
                _elasticSearchWrapperMock.Object,
                _customAddressSorterMock.Object
            );
        }

        [Test]
        public async Task GetListOfAssets_WhenCustomSortingFalse_DoesntUseCustomSort()
        {
            // Arrange
            var query = new GetAssetListRequest
            {
                UseCustomSorting = false
            };

            var elasticSearchResponse = _fixture.Create<SearchResponse<QueryableAsset>>();

            _elasticSearchWrapperMock
                .Setup(x => x.Search<QueryableAsset, GetAssetListRequest>(query))
                .ReturnsAsync(elasticSearchResponse);

            // Act
            var response = await _searchGateway.GetListOfAssets(query);

            // Assert
            response.Should().BeEquivalentTo(elasticSearchResponse.ToResponse());

            _customAddressSorterMock
                .Verify(x => x.FilterResponse(query, It.IsAny<GetAssetListResponse>()), Times.Never);
        }

        [Test]
        public async Task GetListOfAssets_WhenCustomSortingTrue_OverridesPageSize()
        {
            // Arrange
            var query = new GetAssetListRequest
            {
                UseCustomSorting = true
            };

            var elasticSearchResponse = _fixture.Create<SearchResponse<QueryableAsset>>();

            _elasticSearchWrapperMock
                .Setup(x => x.Search<QueryableAsset, GetAssetListRequest>(query))
                .ReturnsAsync(elasticSearchResponse);

            // Act
            var response = await _searchGateway.GetListOfAssets(query);

            // Assert
            query.PageSize.Should().Be(400);
        }

        [Test]
        public async Task GetListOfAssets_WhenCustomSortingTrue_CallsCustomAddressSorter()
        {
            // Arrange
            var query = new GetAssetListRequest
            {
                UseCustomSorting = true
            };

            var elasticSearchResponse = _fixture.Create<SearchResponse<QueryableAsset>>();

            _elasticSearchWrapperMock
                .Setup(x => x.Search<QueryableAsset, GetAssetListRequest>(query))
                .ReturnsAsync(elasticSearchResponse);

            // Act
            var response = await _searchGateway.GetListOfAssets(query);

            // Assert
            response.Should().BeEquivalentTo(elasticSearchResponse.ToResponse());

            _customAddressSorterMock
                .Verify(x => x.FilterResponse(query, It.IsAny<GetAssetListResponse>()), Times.Once);
        }

    }
}
