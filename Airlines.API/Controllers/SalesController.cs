using Airlines.API.Contracts.Sales;
using Airlines.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISellService _sellService;

    public SalesController(ISellService sellService)
    {
        _sellService = sellService;
    }

    // Preview sale (Validate step 1 + price calculation)
    // POST: /api/sales/preview
    [HttpPost("preview")]
    public async Task<ActionResult<SalePreviewResponse>> PreviewSale(
        [FromBody] SalePreviewRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sellService.ValidateSellStep1Async(request, cancellationToken);

        if (!result.IsValid)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    // Confirm sale (create Buy + Tickets, capacity check)
    // POST: /api/sales/confirm
    [HttpPost("confirm")]
    public async Task<ActionResult<ConfirmSaleResponse>> ConfirmSale(
        [FromBody] ConfirmSaleRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sellService.ConfirmSaleAsync(request, cancellationToken);

        if (!result.IsAccepted)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}

