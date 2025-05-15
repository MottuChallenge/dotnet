using System.ComponentModel.DataAnnotations;

namespace MottuGrid_Dotnet.Domain.DTO.Request
{
    public class SectionRequest
    {
        [Required(ErrorMessage = "A cor é obrigatória.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "A área é obrigatória.")]
        public double Area { get; set; }

        [Required(ErrorMessage = "O ID do pátio é obrigatório.")]
        public Guid YardId { get; set; }
    }
}
