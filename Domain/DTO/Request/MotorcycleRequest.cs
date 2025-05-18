using System.ComponentModel.DataAnnotations;
using MottuGrid_Dotnet.Domain.Enums;

namespace MottuGrid_Dotnet.Domain.DTO.Request
{
    public class MotorcycleRequest
    {
        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "O tipo de motor é obrigatório.")]
        public string EngineType { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória.")]
        public string Plate { get; set; }

        [Required(ErrorMessage = "A data da última revisão é obrigatória.")]
        public DateTime LastRevisionDate { get; set; }

        [Required(ErrorMessage = "O ID da seção é obrigatório.")]
        public Guid SectionId { get; set; }
    }
}
