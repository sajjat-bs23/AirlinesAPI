using Airlines.API.Contracts.Flights;
using Airlines.API.Contracts.Responses;
using Airlines.API.Repositories;

namespace Airlines.API.Services;

public class FlightSearchService : IFlightSearchService
{
    private readonly IFlightRepository _flightRepository;

    public FlightSearchService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<PagedResult<FlightSearchResultDto>> SearchAsync(
        FlightSearchRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _flightRepository.SearchFlightsAsync(request, cancellationToken);
    }
}

