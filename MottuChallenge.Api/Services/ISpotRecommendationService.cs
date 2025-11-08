using MottuChallenge.Application.DTOs.Response;

namespace MottuChallenge.Api.Services
{
    public interface ISpotRecommendationService
    {
        Task<RecommendSpotResult?> RecommendSpotAsync(Guid sectorId, DateTime reviewDate, CancellationToken cancellationToken = default);
    }
}
