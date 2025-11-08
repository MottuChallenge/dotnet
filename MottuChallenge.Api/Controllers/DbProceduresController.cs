

using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.Interfaces;

namespace MottuChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/db")]
    public class DbProceduresController : ControllerBase
    {
        private readonly IDbProceduresService _dbProceduresService;

        public DbProceduresController(IDbProceduresService dbProceduresService)
        {
            _dbProceduresService = dbProceduresService;
        }

        [HttpGet("spot/{id}")]
        public async Task<IActionResult> GetSpotJson(string id)
        {
            var result = await _dbProceduresService.GetSpotJsonAsync(id);
            if (string.IsNullOrWhiteSpace(result))
                return NotFound(new { error = "Spot not found or empty result" });

            // The function already returns JSON text (as VARCHAR2). Return as application/json raw content.
            return Content(result, "application/json");
        }

        [HttpGet("validate-plate/{plate}")]
        public async Task<IActionResult> ValidatePlate(string plate)
        {
            var result = await _dbProceduresService.ValidatePlateAsync(plate);
            return Ok(new { message = result });
        }

        [HttpPost("spots-with-sector")]
        public async Task<IActionResult> ExecuteSpotsWithSectorJson()
        {
            await _dbProceduresService.ExecuteSpotsWithSectorJsonAsync();
            return Ok(new { message = "Procedure executed. If it prints via DBMS_OUTPUT, that output is not returned by this endpoint." });
        }

        [HttpPost("report")]
        public async Task<IActionResult> ExecuteRelatorio()
        {
            await _dbProceduresService.ExecuteRelatorioMotosPorVagaAsync();
            return Ok(new { message = "Report procedure executed." });
        }
    }
}
