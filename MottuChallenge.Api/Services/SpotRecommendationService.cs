using MottuChallenge.Application.DTOs.Response;

namespace MottuChallenge.Api.Services
{
    public class SpotRecommendationService : ISpotRecommendationService
    {
        private readonly Application.Repositories.ISectorRepository _sectorRepo;
        private readonly Application.Repositories.IMotorcycleRepository _motoRepo;
        private readonly ILogger<SpotRecommendationService> _logger;

        public SpotRecommendationService(
            Application.Repositories.ISectorRepository sectorRepo,
            Application.Repositories.IMotorcycleRepository motoRepo,
            ILogger<SpotRecommendationService> logger)
        {
            _sectorRepo = sectorRepo ?? throw new ArgumentNullException(nameof(sectorRepo));
            _motoRepo = motoRepo ?? throw new ArgumentNullException(nameof(motoRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RecommendSpotResult?> RecommendSpotAsync(Guid sectorId, DateTime reviewDate, CancellationToken cancellationToken = default)
        {
            // Validate
            if (sectorId == Guid.Empty) throw new ArgumentException("sectorId is required", nameof(sectorId));

            var sector = await _sectorRepo.GetSectorByIdAsync(sectorId);
            if (sector == null)
            {
                throw new KeyNotFoundException("Sector not found");
            }

            // find free spots
            var freeSpots = sector.Spots?.Where(s => s.Status == Domain.Enums.SpotStatus.FREE).ToList();
            if (freeSpots == null || !freeSpots.Any())
            {
                throw new KeyNotFoundException("No free spots in sector");
            }

            var motorcycles = await _motoRepo.GetMotorcyclesBySectorIdAsync(sectorId);

            double bestScore = double.MinValue;
            var bestSpot = freeSpots.First();

            foreach (var spot in freeSpots)
            {
                cancellationToken.ThrowIfCancellationRequested();

                double score = 0.0;
                foreach (var m in motorcycles)
                {
                    if (m.Spot == null) continue;
                    var daysDiff = Math.Abs((m.LastRevisionDate - reviewDate).TotalDays);
                    var weight = 1.0 / (1.0 + daysDiff);
                    var dx = spot.X - m.Spot.X;
                    var dy = spot.Y - m.Spot.Y;
                    var dist = Math.Sqrt(dx * dx + dy * dy);
                    score += weight / (1.0 + dist);
                }

                if (score > bestScore)
                {
                    bestScore = score;
                    bestSpot = spot;
                }
            }
            
            var normalized = (float)(bestScore <= 0 ? 0.0 : 1.0 - 1.0 / (1.0 + bestScore));

            return new RecommendSpotResult { SpotId = bestSpot.SpotId, Score = normalized };
        }
    }
}
