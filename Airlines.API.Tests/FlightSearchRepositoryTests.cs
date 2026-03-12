using Airlines.API.Contracts.Flights;
using Airlines.API.Contracts.Responses;
using Airlines.API.Repositories;
using Airlines.API.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Airlines.API.Tests;

public class FlightSearchRepositoryTests
{
    private static PagedResult<FlightSearchResultDto> StubResult(params FlightSearchResultDto[] flights)
    {
        return new PagedResult<FlightSearchResultDto>
        {
            Items = flights,
            PageNumber = 1,
            PageSize = 50,
            TotalCount = flights.Length
        };
    }

    [Fact]
    public async Task FlightSearch_MapCriteriaToQuery_DateOnly_ReturnsCorrectFilter()
    {
        var repoMock = new Mock<IFlightRepository>();
        var service = new FlightSearchService(repoMock.Object);

        var request = new FlightSearchRequest
        {
            FlightDate = new DateOnly(2026, 5, 1),
            PageNumber = 1,
            PageSize = 50
        };

        repoMock
            .Setup(r => r.SearchFlightsAsync(
                It.Is<FlightSearchRequest>(req =>
                    req.FlightDate == request.FlightDate &&
                    req.FlightNumber == null &&
                    req.DepartureAirportId == null &&
                    req.ArrivalAirportId == null),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(StubResult(new FlightSearchResultDto
            {
                FlightId = 2,
                FlightNumber = "AB1234",
                FlightDate = request.FlightDate!.Value
            }));

        var result = await service.SearchAsync(request);

        result.Items.Should().HaveCount(1);
        result.Items.Single().FlightDate.Should().Be(request.FlightDate);
        repoMock.VerifyAll();
    }

    [Fact]
    public async Task FlightSearch_MapCriteriaToQuery_DateAndFlightNum_ReturnsCorrectFilter()
    {
        var repoMock = new Mock<IFlightRepository>();
        var service = new FlightSearchService(repoMock.Object);

        var request = new FlightSearchRequest
        {
            FlightDate = new DateOnly(2026, 5, 1),
            FlightNumber = "AB1234",
            PageNumber = 1,
            PageSize = 50
        };

        repoMock
            .Setup(r => r.SearchFlightsAsync(
                It.Is<FlightSearchRequest>(req =>
                    req.FlightDate == request.FlightDate &&
                    req.FlightNumber == request.FlightNumber &&
                    req.DepartureAirportId == null &&
                    req.ArrivalAirportId == null),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(StubResult(new FlightSearchResultDto
            {
                FlightId = 2,
                FlightNumber = "AB1234",
                FlightDate = request.FlightDate!.Value
            }));

        var result = await service.SearchAsync(request);

        result.Items.Should().HaveCount(1);
        var flight = result.Items.Single();
        flight.FlightNumber.Should().Be("AB1234");
        flight.FlightDate.Should().Be(request.FlightDate);
        repoMock.VerifyAll();
    }

    [Fact]
    public async Task FlightSearch_MapCriteriaToQuery_DateAndDepArr_ReturnsCorrectFilter()
    {
        var repoMock = new Mock<IFlightRepository>();
        var service = new FlightSearchService(repoMock.Object);

        var request = new FlightSearchRequest
        {
            FlightDate = new DateOnly(2026, 5, 1),
            DepartureAirportId = 10,
            ArrivalAirportId = 20,
            PageNumber = 1,
            PageSize = 50
        };

        repoMock
            .Setup(r => r.SearchFlightsAsync(
                It.Is<FlightSearchRequest>(req =>
                    req.FlightDate == request.FlightDate &&
                    req.FlightNumber == null &&
                    req.DepartureAirportId == request.DepartureAirportId &&
                    req.ArrivalAirportId == request.ArrivalAirportId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(StubResult(new FlightSearchResultDto
            {
                FlightId = 2,
                FlightNumber = "AB1234",
                FlightDate = request.FlightDate!.Value,
                DepartureAirportId = 10,
                ArrivalAirportId = 20
            }));

        var result = await service.SearchAsync(request);

        result.Items.Should().HaveCount(1);
        var flight = result.Items.Single();
        flight.DepartureAirportId.Should().Be(10);
        flight.ArrivalAirportId.Should().Be(20);
        flight.FlightDate.Should().Be(request.FlightDate);
        repoMock.VerifyAll();
    }

    [Fact]
    public async Task FlightSearch_EmptyResult_ReturnsEmptyListAndNoThrow()
    {
        var repoMock = new Mock<IFlightRepository>();
        var service = new FlightSearchService(repoMock.Object);

        var request = new FlightSearchRequest
        {
            FlightDate = new DateOnly(2030, 1, 1),
            PageNumber = 1,
            PageSize = 50
        };

        repoMock
            .Setup(r => r.SearchFlightsAsync(
                It.IsAny<FlightSearchRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(StubResult());

        var result = await service.SearchAsync(request);

        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
        repoMock.Verify(r => r.SearchFlightsAsync(
            It.IsAny<FlightSearchRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}

