using System.Text;
using Airlines.API.Contracts.Passengers;
using Airlines.API.Models;
using Airlines.API.Repositories;

namespace Airlines.API.Services;

public class PassengerImportService : IPassengerImportService
{
    private static readonly string[] CsvHeaders =
    [
        "FirstName",
        "LastName",
        "Address",
        "City",
        "Country",
        "ZipCode",
        "Telephone",
        "Email"
    ];

    private readonly IPassengerRepository _passengerRepository;

    public PassengerImportService(IPassengerRepository passengerRepository)
    {
        _passengerRepository = passengerRepository;
    }

    public byte[] GetImportTemplateCsv()
    {
        var sb = new StringBuilder();

        sb.AppendLine(string.Join(",", CsvHeaders));

        var sampleRow = new[]
        {
            "JOHN",
            "DOE",
            "123 MAIN STREET",
            "NEW YORK",
            "USA",
            "10001",
            "+1-555-0000",
            "john.doe@example.com"
        };

        sb.AppendLine(string.Join(",", sampleRow.Select(EscapeCsv)));

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    public async Task<PassengerImportResult> ImportFromCsvAsync(Stream csvStream, CancellationToken cancellationToken = default)
    {
        var result = new PassengerImportResult();

        using var reader = new StreamReader(csvStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);

        var headerLine = await reader.ReadLineAsync();
        if (headerLine is null)
        {
            result.IsHeaderValid = false;
            result.HeaderErrorMessage = "CSV file is empty";
            return result;
        }

        var headerColumns = SplitCsvLine(headerLine).ToArray();
        if (!HeadersMatch(headerColumns, CsvHeaders))
        {
            result.IsHeaderValid = false;
            result.HeaderErrorMessage = "CSV header is invalid. Expected columns: " + string.Join(", ", CsvHeaders);
            return result;
        }

        var existingKeyData = await _passengerRepository.GetPassengerKeyDataAsync(cancellationToken);
        var existingKeys = new HashSet<string>(
            existingKeyData.Select(k => BuildDuplicateKey(k.FirstName, k.LastName, k.Address, k.City, k.Country, k.ZipCode, k.Email)),
            StringComparer.OrdinalIgnoreCase);

        var fileKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var newPassengers = new List<Passenger>();

        var lineNumber = 1;
        while (!reader.EndOfStream)
        {
            lineNumber++;
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            result.TotalRows++;

            var columns = SplitCsvLine(line).ToArray();
            if (columns.Length != CsvHeaders.Length)
            {
                result.SkippedInvalidCount++;
                result.RowErrors.Add($"Line {lineNumber}: expected {CsvHeaders.Length} columns but found {columns.Length}.");
                continue;
            }

            var firstName = Normalize(columns[0]);
            var lastName = Normalize(columns[1]);
            var address = Normalize(columns[2]);
            var city = Normalize(columns[3]);
            var country = Normalize(columns[4]);
            var zipCode = Normalize(columns[5]);
            var telephone = Normalize(columns[6]);
            var email = Normalize(columns[7]);

            var rowErrors = ValidateRow(firstName, lastName, address, city, country, zipCode, telephone, email);

            if (rowErrors.Count > 0)
            {
                result.SkippedInvalidCount++;
                result.RowErrors.Add($"Line {lineNumber}: {string.Join(" | ", rowErrors)}");
                continue;
            }

            var key = BuildDuplicateKey(firstName, lastName, address, city, country, zipCode, email);

            if (existingKeys.Contains(key))
            {
                result.SkippedDuplicateCount++;
                result.RowErrors.Add($"Line {lineNumber}: duplicate passenger already exists in database.");
                continue;
            }

            if (fileKeys.Contains(key))
            {
                result.SkippedDuplicateCount++;
                result.RowErrors.Add($"Line {lineNumber}: duplicate passenger within the uploaded file.");
                continue;
            }

            fileKeys.Add(key);

            newPassengers.Add(new Passenger
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                Country = country,
                ZipCode = zipCode,
                Telephone = telephone,
                Email = email
            });
        }

        if (newPassengers.Count == 0)
        {
            return result;
        }

        result.InsertedCount = await _passengerRepository.AddPassengersAsync(newPassengers, cancellationToken);
        return result;
    }

    private static string EscapeCsv(string value)
    {
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
        {
            var escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

        return value;
    }

    private static IEnumerable<string> SplitCsvLine(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            yield break;
        }

        var sb = new StringBuilder();
        var inQuotes = false;

        for (var i = 0; i < line.Length; i++)
        {
            var c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    sb.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                yield return sb.ToString();
                sb.Clear();
            }
            else
            {
                sb.Append(c);
            }
        }

        yield return sb.ToString();
    }

    private static bool HeadersMatch(string[] actual, string[] expected)
    {
        if (actual.Length != expected.Length)
        {
            return false;
        }

        for (var i = 0; i < expected.Length; i++)
        {
            if (!string.Equals(actual[i].Trim(), expected[i], StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

        return true;
    }

    private static string Normalize(string value) => value.Trim();

    private static List<string> ValidateRow(
        string firstName,
        string lastName,
        string address,
        string city,
        string country,
        string zipCode,
        string telephone,
        string email)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(firstName))
            errors.Add("FirstName is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            errors.Add("LastName is required.");
        if (string.IsNullOrWhiteSpace(address))
            errors.Add("Address is required.");
        if (string.IsNullOrWhiteSpace(city))
            errors.Add("City is required.");
        if (string.IsNullOrWhiteSpace(country))
            errors.Add("Country is required.");
        if (string.IsNullOrWhiteSpace(zipCode))
            errors.Add("ZipCode is required.");
        if (string.IsNullOrWhiteSpace(telephone))
            errors.Add("Telephone is required.");
        if (string.IsNullOrWhiteSpace(email))
            errors.Add("Email is required.");
        else if (!IsValidEmail(email))
            errors.Add("Email format is invalid.");

        return errors;
    }

    private static bool IsValidEmail(string email) =>
        !string.IsNullOrWhiteSpace(email) &&
        email.Contains('@') &&
        email.Contains('.');

    private static string BuildDuplicateKey(
        string firstName,
        string lastName,
        string address,
        string city,
        string country,
        string zipCode,
        string email)
    {
        return string.Join("|", new[]
        {
            firstName.ToUpperInvariant(),
            lastName.ToUpperInvariant(),
            address.ToUpperInvariant(),
            city.ToUpperInvariant(),
            country.ToUpperInvariant(),
            zipCode.ToUpperInvariant(),
            email.ToUpperInvariant()
        });
    }
}
