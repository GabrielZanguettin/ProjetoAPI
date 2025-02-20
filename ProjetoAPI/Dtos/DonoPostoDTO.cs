using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Dtos
{
    public class DonoPostoDTO
    {
        [Required]
        public string? email { get; set; }
        [Required]
        public string? senha { get; set; }
    }
}
