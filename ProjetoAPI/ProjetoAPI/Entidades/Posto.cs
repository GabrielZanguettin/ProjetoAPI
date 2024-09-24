using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoAPI.Entidades
{
    public class Posto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PostoId { get; set; }
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
        [Required]
        public float? Gasolina { get; set; }
        [Required]
        public float? Diesel {  get; set; }
        [Required]
        public float? Etanol {  get; set; }
        [JsonIgnore]
        public int DonoPostoId { get; set; }
        [JsonIgnore]
        public DonoPosto? DonoPosto { get; set; }
    }
}
