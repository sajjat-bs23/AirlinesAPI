using Airlines.API.Contracts.Responses;
using Airlines.API.Contracts.Tickets;
using Airlines.API.Repositories;

namespace Airlines.API.Services;

public class TicketSearchService : ITicketSearchService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketSearchService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<PagedResult<TicketSearchResultDto>> SearchAsync(
        TicketSearchRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _ticketRepository.SearchTicketsAsync(request, cancellationToken);
    }
}

