using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Dtos
{
    public class PostoDTO
    {
        [Required]
        [MinLength(14), MaxLength(14, ErrorMessage = "CNPJ inválido")]
        public string? CNPJ { get; set; }
        [Required]
        [MaxLength(60, ErrorMessage = "Endereço muito grande")]
        public string? Endereco { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Telefone inválido")]
        public string? Telefone { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Descrição muito grande")]
        public string? HoraFuncionamento { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Bandeira inválida")]
        public string? Bandeira { get; set; }
    }
}
