using Airlines.API.Contracts.Flights;
using Airlines.API.Contracts.Responses;
using Airlines.API.Contracts.Tickets;
using Airlines.API.Controllers;
using Airlines.API.Repositories;
using Airlines.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Airlines.API.Tests;

public class TicketSearchTests
{
    private static PagedResult<TicketSearchResultDto> StubTickets(
        int pageNumber,
        int pageSize,
        params TicketSearchResultDto[] tickets)
    {
        return new PagedResult<TicketSearchResultDto>
        {
            Items = tickets,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = tickets.Length
        };
    }

    [Fact]
    public async Task TicketSearch_InvalidDate_ReturnsValidationErrorWithoutDbCall()
    {
        var serviceMock = new Mock<ITicketSearchService>(MockBehavior.Strict);
        var controller = new TicketsController(serviceMock.Object);

        // Simulate invalid model state for FlightDate (e.g., bad format "01/01/26")
        controller.ModelState.AddModelError("FlightDate", "Invalid date format");

        var request = new TicketSearchRequest();

        var actionResult = await controller.SearchTickets(request, CancellationToken.None);

        var badRequest = actionResult.Result as BadRequestObjectResult;
        badRequest.Should().NotBeNull();

        var response = badRequest!.Value as ApiResponse<PagedResult<TicketSearchResultDto>>;
        response.Should().NotBeNull();
        response!.Success.Should().BeFalse();
        response.Message.Should().Be("Invalid date format");

        serviceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task TicketSearch_MapCriteriaToQuery_ByTicketId_ReturnsCorrectFilter()
    {
        var repoMock = new Mock<ITicketRepository>();
        var service = new TicketSearchService(repoMock.Object);

        var request = new TicketSearchRequest
        {
            TicketId = 1,
            PageNumber = 1,
            PageSize = 20
        };

        repoMock
            .Setup(r => r.SearchTicketsAsync(
                It.Is<TicketSearchRequest>(req =>
                    req.TicketId == request.TicketId &&
                    req.ClientId == null &&
                    req.FirstName == null &&
                    req.LastName == null &&
                    req.FlightNumber == null &&
                    req.FlightDate == null &&
                    req.PageNumber == request.PageNumber &&
                    req.PageSize == request.PageSize),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(StubTickets(request.PageNumber, request.PageSize,
                new TicketSearchResultDto { TicketId = 1, ClientId = 123 }));

        var result = await service.SearchAsync(request, CancellationToken.None);

        result.Items.Should().HaveCount(1);
        result.Items.Single().TicketId.Should().Be(1);
        repoMock.VerifyAll();
    }

    [Fact]
    public async Task TicketSearch_MapCriteriaToQuery_ByClientIdAndFlightNum_ReturnsCorrectFilter()
    {
        var repoMock = new Mock<ITicketRepository>();
        var service = new TicketSearchService(repoMock.Object);

        var request = new TicketSearchRequest
        {
            ClientId = 123,
            FlightNumber = "AB1234",
            PageNumber = 1,
            PageSize = 20
        };

        repoMock
            .Setup(r => r.SearchTicketsAsync(
                It.Is<TicketSearchRequest>(req =>
                    req.TicketId == null &&
                    req.ClientId == request.ClientId &&
                    req.FirstName == null &&
                    req.LastName == null &&
                    req.FlightNumber == request.FlightNumber &&
                    req.FlightDate == null),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(StubTickets(request.PageNumber, request.PageSize,
                new TicketSearchResultDto
                {
                    TicketId = 10,
                    ClientId = 123,
                    FlightNumber = "AB1234"
                }));

        var result = await service.SearchAsync(request, CancellationToken.None);

        result.Items.Should().HaveCount(1);
        var ticket = result.Items.Single();
        ticket.ClientId.Should().Be(123);
        ticket.FlightNumber.Should().Be("AB1234");
        repoMock.VerifyAll();
    }

    [Fact]
    public async Task TicketSearch_MapCriteriaToQuery_ByNameAndDate_ReturnsCorrectFilter()
    {
        var repoMock = new Mock<ITicketRepository>();
        var service = new TicketSearchService(repoMock.Object);

        var request = new TicketSearchRequest
        {
            FirstName = "John",
            LastName = "Doe",
            FlightDate = new DateOnly(2026, 5, 1),
            PageNumber = 1,
            PageSize = 20
        };

        repoMock
            .Setup(r => r.SearchTicketsAsync(
                It.Is<TicketSearchRequest>(req =>
                    req.TicketId == null &&
                    req.ClientId == null &&
                    req.FirstName == request.FirstName &&
                    req.LastName == request.LastName &&
                    req.FlightNumber == null &&
                    req.FlightDate == request.FlightDate),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(StubTickets(request.PageNumber, request.PageSize,
                new TicketSearchResultDto
                {
                    TicketId = 20,
                    ClientId = 123,
                    PassengerFirstName = "John",
                    PassengerLastName = "Doe",
                    FlightDate = request.FlightDate.Value
                }));

        var result = await service.SearchAsync(request, CancellationToken.None);

        result.Items.Should().HaveCount(1);
        var ticket = result.Items.Single();
        ticket.PassengerFirstName.Should().Be("John");
        ticket.PassengerLastName.Should().Be("Doe");
        ticket.FlightDate.Should().Be(request.FlightDate);
        repoMock.VerifyAll();
    }

    [Fact]
    public async Task TicketSearch_Paging_OffsetAndLimitApplied()
    {
        var repoMock = new Mock<ITicketRepository>();
        var service = new TicketSearchService(repoMock.Object);

        var request = new TicketSearchRequest
        {
            ClientId = 123,
            PageNumber = 2,
            PageSize = 20
        };

        repoMock
            .Setup(r => r.SearchTicketsAsync(
                It.Is<TicketSearchRequest>(req =>
                    req.ClientId == request.ClientId &&
                    req.PageNumber == request.PageNumber &&
                    req.PageSize == request.PageSize),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(StubTickets(request.PageNumber, request.PageSize));

        var result = await service.SearchAsync(request, CancellationToken.None);

        result.PageNumber.Should().Be(2);
        result.PageSize.Should().Be(20);
        repoMock.VerifyAll();
    }
}

