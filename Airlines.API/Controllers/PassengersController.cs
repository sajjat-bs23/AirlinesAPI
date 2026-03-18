using Airlines.API.Contracts.Passengers;
using Airlines.API.Contracts.Responses;
using Airlines.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PassengersController : ControllerBase
{
    private const string TemplateFileName = "PassengerImportTemplate.csv";
    private readonly IPassengerImportService _passengerImportService;

    public PassengersController(IPassengerImportService passengerImportService)
    {
        _passengerImportService = passengerImportService;
    }

    /// <summary>
    /// Download a CSV template (with one sample row) for passenger import.
    /// </summary>
    [HttpGet("import/template")]
    public IActionResult DownloadImportTemplate()
    {
        var bytes = _passengerImportService.GetImportTemplateCsv();
        return File(bytes, "text/csv", TemplateFileName);
    }

    /// <summary>
    /// Upload a CSV file to import passengers.
    /// </summary>
    [HttpPost("import")]
    [RequestSizeLimit(10 * 1024 * 1024)] // 10 MB
    public async Task<ActionResult<ApiResponse<PassengerImportResult>>> ImportPassengers(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest(ApiResponse<PassengerImportResult>.Fail("File is required"));
        }

        if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(ApiResponse<PassengerImportResult>.Fail("Only CSV files are supported"));
        }

        PassengerImportResult result;
        try
        {
            await using var stream = file.OpenReadStream();
            result = await _passengerImportService.ImportFromCsvAsync(stream, cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                ApiResponse<PassengerImportResult>.Fail(
                    "An error occurred while saving passengers.",
                    new[] { ex.InnerException?.Message ?? ex.Message }));
        }

        if (!result.IsHeaderValid)
        {
            return BadRequest(ApiResponse<PassengerImportResult>.Fail(result.HeaderErrorMessage ?? "Invalid CSV header"));
        }

        if (result.InsertedCount == 0 && result.TotalRows > 0)
        {
            return Ok(ApiResponse<PassengerImportResult>.Fail(
                "No passengers were imported.",
                result.RowErrors));
        }

        return Ok(ApiResponse<PassengerImportResult>.Ok(result, "Passenger import completed."));
    }
}
