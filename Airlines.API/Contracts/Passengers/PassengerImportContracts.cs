namespace Airlines.API.Contracts.Passengers;

public class PassengerImportResult
{
    public bool IsHeaderValid { get; set; } = true;
    public string? HeaderErrorMessage { get; set; }

    public int TotalRows { get; set; }
    public int InsertedCount { get; set; }
    public int SkippedDuplicateCount { get; set; }
    public int SkippedInvalidCount { get; set; }

    public List<string> RowErrors { get; set; } = new();
}

