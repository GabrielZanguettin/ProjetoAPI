using System.Text.Json.Serialization;

namespace ProjetoAPI.Entidades
{
    public class Posto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PostoId { get; set; }
        public string? CNPJ { get; set; }
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
        public string? HoraFuncionamento { get; set; }
        public string? Bandeira { get; set; }
        [JsonIgnore]
        public int DonoPostoId { get; set; }
        [JsonIgnore]
        public DonoPosto? DonoPosto { get; set; }
    }
}
