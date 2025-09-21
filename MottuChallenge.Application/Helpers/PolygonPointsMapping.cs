using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Helpers
{
    internal static class PolygonPointsMapping
    {
        public static List<PointResponseDto> CreateListOfPointResponseDto(Yard yard)
        {
            var points = new List<PointResponseDto>();

            foreach (var point in yard.Points)
            {
                points.Add(new PointResponseDto { PointOrder = point.PointOrder, X = point.X, Y = point.Y });
            }

            return points;
        }
    }
}
