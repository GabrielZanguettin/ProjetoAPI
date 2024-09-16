using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using ProjetoAPI.Dtos;
using ProjetoAPI.Entidades;
using ProjetoAPI.Services;

namespace ProjetoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostoController : ControllerBase
    {
        private readonly PostoService postoService;
        public PostoController(PostoService postoService)
        {
            this.postoService = postoService;
        }
        [Authorize]
        [HttpPut("editar")]
        public IActionResult Editar(int id, [FromBody] PostoDTO posto)
        {
            if (posto == null)
            {
                return BadRequest("Você não passou um posto");
            }
            var sucesso = postoService.Editar(id, posto);
            if (sucesso)
            {
                return Ok(posto);
            }
            return BadRequest("Erro ao editar");
        }
        [Authorize]
        [HttpDelete("deletar")]
        public IActionResult Remover(int id)
        {
            var posto = postoService.Remover(id);
            if (posto == null)
            {
                return NotFound();
            }
            return Ok(posto);
        }
    }
}
