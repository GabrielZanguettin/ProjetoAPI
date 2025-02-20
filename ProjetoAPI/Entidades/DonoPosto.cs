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
        public string? Nome { get; set; }
        [Required]
        public string? CPF { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? SenhaHasheada { get; set; }
        [Required]
        public string? Telefone { get; set; }
        [JsonIgnore]
        public ICollection<Posto>? Postos { get; set; } = new List<Posto>();
    }
}