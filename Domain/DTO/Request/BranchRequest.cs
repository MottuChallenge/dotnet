using System.ComponentModel.DataAnnotations;

namespace MottuGrid_Dotnet.Domain.DTO.Request
{
  
    public class BranchRequest
    {
        [Required(ErrorMessage = "O nome da filial é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da filial não pode exceder 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(14, ErrorMessage = "CNPJ tem que ter 14 caracters")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "Telefone inválido.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O cep de endereço é obrigatório.")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O numero de endereço é obrigatório.")]
        public string Number { get; set; }
    }

}
