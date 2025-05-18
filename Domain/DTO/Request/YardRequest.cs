using System.ComponentModel.DataAnnotations;

namespace MottuGrid_Dotnet.Domain.DTO.Request
{
    public class YardRequest
    {
        [Required(ErrorMessage = "O nome do pátio é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do pátio não pode exceder 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A área do pátio é obrigatória.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "A área deve ser maior que zero.")]
        public double Area { get; set; }

        [Required(ErrorMessage = "O id da filial é obrigatório.")]
        public Guid BranchId { get; set; }

        [Required(ErrorMessage = "O cep de endereço é obrigatório.")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O número do endereço é obrigatório.")]
        public string Number { get; set; }
    }
}
