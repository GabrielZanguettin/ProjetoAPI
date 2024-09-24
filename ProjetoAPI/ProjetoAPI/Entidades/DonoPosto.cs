using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Entidades
{
    public class DonoPosto
    {
        [JsonIgnore]
        public int DonoPostoId { get; set; }
        [Required]
        [MaxLength(35, ErrorMessage = "Nome muito grande")]
        public string? Nome { get; set; }
        [Required]
        [MinLength(11), MaxLength(11, ErrorMessage = "CPF inválido")]
        public string? CPF { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Email muito curto")]
        public string? Email { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Senha muito curta")]
        public string? SenhaHasheada { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Telefone inválido")]
        public string? Telefone { get; set; }
        [JsonIgnore]
        public ICollection<Posto>? Postos { get; set; } = new List<Posto>();
    }
}
