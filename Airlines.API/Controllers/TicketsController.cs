using Airlines.API.Contracts.Responses;
using Airlines.API.Contracts.Tickets;
using Airlines.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketSearchService _ticketSearchService;

    public TicketsController(ITicketSearchService ticketSearchService)
    {
        _ticketSearchService = ticketSearchService;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<TicketSearchResultDto>>>> SearchTickets(
        [FromQuery] TicketSearchRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<PagedResult<TicketSearchResultDto>>.Fail(
                "Invalid date format"));
        }

        var hasAnyCriteria =
            request.TicketId is not null ||
            request.ClientId is not null ||
            !string.IsNullOrWhiteSpace(request.FirstName) ||
            !string.IsNullOrWhiteSpace(request.LastName) ||
            !string.IsNullOrWhiteSpace(request.FlightNumber) ||
            request.FlightDate is not null;

        if (!hasAnyCriteria)
        {
            return Ok(ApiResponse<PagedResult<TicketSearchResultDto>>.Fail(
                "INSERT THE DATA TO DO A RESEARCH"));
        }

        var result = await _ticketSearchService.SearchAsync(request, cancellationToken);

        if (result.TotalCount == 0)
        {
            return Ok(ApiResponse<PagedResult<TicketSearchResultDto>>.Fail("No ticket found"));
        }

        return Ok(ApiResponse<PagedResult<TicketSearchResultDto>>.Ok(result));
    }
}

