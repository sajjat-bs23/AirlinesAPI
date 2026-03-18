using Airlines.API.Contracts.Passengers;
using Airlines.API.Models;

namespace Airlines.API.Repositories;

public interface IPassengerRepository
{
    Task<IReadOnlyList<PassengerKeyData>> GetPassengerKeyDataAsync(CancellationToken cancellationToken = default);
    Task<int> AddPassengersAsync(IReadOnlyList<Passenger> passengers, CancellationToken cancellationToken = default);
}
