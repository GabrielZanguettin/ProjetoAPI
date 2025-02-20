using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Dtos
{
    public class PostoDTO
    {
        [Required]
        public string? CNPJ { get; set; }
        [Required]
        public string? Endereco { get; set; }
        [Required]
        public string? Telefone { get; set; }
        [Required]
        public string? HoraFuncionamento { get; set; }
        [Required]
        public string? NomePosto { get; set; }
    }
}