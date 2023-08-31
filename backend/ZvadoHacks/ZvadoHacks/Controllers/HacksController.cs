using Microsoft.AspNetCore.Mvc;
using ZvadoHacks.Infrastructure.Extensions;
using ZvadoHacks.Models.PortScanner;
using ZvadoHacks.Models.Shared;
using ZvadoHacks.Services;

namespace ZvadoHacks.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HacksController : ControllerBase
    {
        private readonly IPortScannerService _portScannerService;

        public HacksController(IPortScannerService portScannerService)
        {
            _portScannerService = portScannerService;
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> PortScan(PortScanRequest request)
        {
            var response = new BaseResponse<PortScanResponse>();
            try
            {
                var userId = HttpContext.GetUserId();

                _ = Task.Run(async () => await _portScannerService.Scan(request, userId));

                return Ok(response.Success());
            }
            catch (Exception e)
            {
                return Ok(response.Error(e.Message));
            }
        }
    }
}
