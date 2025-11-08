namespace MottuChallenge.Application.DTOs.Request;

public class RecommendSpotRequest
{
    public DateTime ReviewDate { get; set; }
    public Guid SectorId { get; set; }
}