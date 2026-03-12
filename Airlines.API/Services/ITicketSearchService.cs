using Airlines.API.Contracts.Responses;
using Airlines.API.Contracts.Tickets;

namespace Airlines.API.Services;

public interface ITicketSearchService
{
    Task<PagedResult<TicketSearchResultDto>> SearchAsync(
        TicketSearchRequest request,
        CancellationToken cancellationToken = default);
}

