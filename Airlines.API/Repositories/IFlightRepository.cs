using Airlines.API.Contracts.Flights;
using Airlines.API.Contracts.Responses;

namespace Airlines.API.Repositories;

public interface IFlightRepository
{
    Task<PagedResult<FlightSearchResultDto>> SearchFlightsAsync(
        FlightSearchRequest request,
        CancellationToken cancellationToken = default);
}

