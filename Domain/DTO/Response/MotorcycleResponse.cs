using MottuGrid_Dotnet.Domain.Entities;

namespace MottuGrid_Dotnet.Domain.DTO.Response
{
    public class MotorcycleResponse
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string EngineType { get; set; }
        public string Plate { get; set; }
        public DateTime LastRevisionDate { get; set; }
        public Guid SectionId { get; set; }

        public MotorcycleResponse(Guid id, string model, string engineType, string plate, DateTime lastRevisionDate, Guid sectionId)
        {
            Id = id;
            Model = model;
            EngineType = engineType;
            Plate = plate;
            LastRevisionDate = lastRevisionDate;
            SectionId = sectionId;
        }

        public static MotorcycleResponse FromEntity(Motorcycle motorcycle)
        {
            return new MotorcycleResponse(
                motorcycle.Id,
                motorcycle.Model,
                motorcycle.EngineType.ToString(),
                motorcycle.Plate,
                motorcycle.LastRevisionDate,
                motorcycle.SectionId
            );
        }
    }
}
