using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Dtos
{
    public class PostoDTOGas
    {
        [Required]
        public float? Gasolina { get; set; }
        [Required]
        public float? Diesel { get; set; }
        [Required]
        public float? Etanol { get; set; }
    }
}
