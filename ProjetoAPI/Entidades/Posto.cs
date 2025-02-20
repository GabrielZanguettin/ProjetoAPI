using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Entidades
{
    public class Posto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PostoId { get; set; }
        [Required]
        public string? NomePosto { get; set; }
        [Required]
        public string? CNPJ { get; set; }
        [Required]
        public string? Endereco { get; set; }
        [Required]
        public string? Telefone { get; set; }
        [Required]
        public string? HoraFuncionamento { get; set; }
        [Required]
        public float? Gasolina { get; set; }
        [Required]
        public float? Diesel { get; set; }
        [Required]
        public float? Etanol { get; set; }
        [JsonIgnore]
        public int DonoPostoId { get; set; }
        [JsonIgnore]
        public DonoPosto? DonoPosto { get; set; }
    }
}