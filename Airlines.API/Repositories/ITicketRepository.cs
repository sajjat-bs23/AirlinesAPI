using Airlines.API.Contracts.Responses;
using Airlines.API.Contracts.Tickets;

namespace Airlines.API.Repositories;

public interface ITicketRepository
{
    Task<PagedResult<TicketSearchResultDto>> SearchTicketsAsync(
        TicketSearchRequest request,
        CancellationToken cancellationToken = default);
}

