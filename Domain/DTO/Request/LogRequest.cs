using System.ComponentModel.DataAnnotations;

namespace MottuGrid_Dotnet.Domain.DTO.Request
{
    public class LogRequest
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public Guid MotorcycleId { get; set; }
    }
}
