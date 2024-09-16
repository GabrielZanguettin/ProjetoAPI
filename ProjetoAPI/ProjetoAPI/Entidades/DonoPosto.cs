using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Entidades
{
    public class DonoPosto
    {
        [JsonIgnore]
        public int DonoPostoId { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Email { get; set; }
        public string? SenhaHasheada { get; set; }
        public string? Telefone { get; set; }
        [JsonIgnore]
        public ICollection<Posto>? Postos { get; set; } = new List<Posto>();
    }
}
