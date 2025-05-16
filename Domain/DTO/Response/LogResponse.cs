using MottuGrid_Dotnet.Domain.Entities;

namespace MottuGrid_Dotnet.Domain.DTO.Response
{
    public class LogResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid MotorcycleId { get; set; }

        public static LogResponse FromEntity(Log log)
        {
            return new LogResponse
            {
                Id = log.Id,
                Message = log.Message,
                CreatedAt = log.CreatedAt,
                MotorcycleId = log.MotorcycleId
            };
        }
    }
}
