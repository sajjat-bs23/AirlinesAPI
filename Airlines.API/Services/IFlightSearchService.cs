using Airlines.API.Contracts.Flights;
using Airlines.API.Contracts.Responses;

namespace Airlines.API.Services;

public interface IFlightSearchService
{
    Task<PagedResult<FlightSearchResultDto>> SearchAsync(
        FlightSearchRequest request,
        CancellationToken cancellationToken = default);
}

