using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Api.Services;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;

namespace MottuChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(2.0)]
    public class MlController : ControllerBase
    {
        private readonly Application.Repositories.ISectorRepository _sectorRepo;
        private readonly Application.Repositories.IMotorcycleRepository _motoRepo;
        private readonly ISpotRecommendationService _recommendationService;
        private readonly ILogger<MlController> _logger;

        public MlController(
            ISpotRecommendationService recommendationService,
            Application.Repositories.ISectorRepository sectorRepo,
            Application.Repositories.IMotorcycleRepository motoRepo,
            ILogger<MlController> logger)
        {
            _recommendationService = recommendationService;
            _sectorRepo = sectorRepo;
            _motoRepo = motoRepo;
            _logger = logger;
        }
        
        [HttpPost("recommend-spot")]
        public async Task<ActionResult<RecommendSpotResult>> RecommendSpot([FromBody] RecommendSpotRequest req, System.Threading.CancellationToken cancellationToken = default)
        {
            if (req is null || req.SectorId == Guid.Empty) return BadRequest();

            try
            {
                var result = await _recommendationService.RecommendSpotAsync(req.SectorId, req.ReviewDate, cancellationToken);
                if (result == null) return NotFound("No recommendation available");

                return Ok(new RecommendSpotResult { SpotId = result.SpotId, Score = result.Score });
            }
            catch (KeyNotFoundException knf)
            {
                _logger.LogWarning(knf, "RecommendSpot: resource not found");
                return NotFound(knf.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RecommendSpot failed");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
