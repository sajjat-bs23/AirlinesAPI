using Airlines.API.Contracts.Passengers;

namespace Airlines.API.Services;

public interface IPassengerImportService
{
    byte[] GetImportTemplateCsv();
    Task<PassengerImportResult> ImportFromCsvAsync(Stream csvStream, CancellationToken cancellationToken = default);
}
