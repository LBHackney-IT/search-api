using FluentAssertions;
using HousingSearchApi.V1.Boundary.Requests;
using HousingSearchApi.V1.Infrastructure.Sorting;
using Xunit;

namespace HousingSearchApi.Tests.V1.Helper
{
    public class PersonListSortFactoryTests
    {
        private PersonListSortFactory _sut;

        public PersonListSortFactoryTests()
        {
            _sut = new PersonListSortFactory();
        }

        [Fact]
        public void ShouldNotSortAsDefault()
        {
            // Arrange + Act
            var result = _sut.Create(new GetPersonListRequest());

            // Assert
            result.Should().BeOfType(typeof(DefaultSort));
        }

        [Fact]
        public void ShouldReturnSurnameAscWhenRequestSurnameAndAsc()
        {
            // Arrange + Act
            var result = _sut.Create(new GetPersonListRequest { SortBy = "surname", IsDesc = false });

            // Assert
            result.Should().BeOfType(typeof(SurnameAsc));
        }

        [Fact]
        public void ShouldReturnSurnameDescWhenRequestSurnameAndDesc()
        {
            // Arrange + Act
            var result = _sut.Create(new GetPersonListRequest { SortBy = "surname", IsDesc = true });

            // Assert
            result.Should().BeOfType(typeof(SurnameDesc));
        }
    }
}