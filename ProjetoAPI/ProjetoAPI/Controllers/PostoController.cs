using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using Org.BouncyCastle.Crypto.Generators;
using ProjetoAPI.Dtos;
using ProjetoAPI.Entidades;
using ProjetoAPI.Services;
using System.Security.Claims;

namespace ProjetoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostoController : ControllerBase
    {
        private readonly PostoService postoService;
        private readonly ISessionFactory sessionFactory;
        public PostoController(PostoService postoService, ISessionFactory sessionFactory)
        {
            this.postoService = postoService;
            this.sessionFactory = sessionFactory;
        }
        [HttpGet("getporpreco")]
        public IActionResult GetPostos()
        {
            try
            {
                var postos = postoService.GetPostos();
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("getpordiesel")]
        public IActionResult GetPostos2()
        {
            try
            {
                var postos = postoService.GetPostos2();
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("getporetanol")]
        public IActionResult GetPostos3()
        {
            try
            {
                var postos = postoService.GetPostos3();
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("getporid")]
        public IActionResult GetPostos4()
        {
            try
            {
                var postos = postoService.GetPostos4();
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
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
        [HttpPut("editarpreco")]
        public IActionResult EditarValores(int id, [FromBody] PostoDTOGas postodtogas)
        {
            if (postodtogas == null)
            {
                return BadRequest("Você não passou um posto");
            }
            var sucesso = postoService.EditarPreco(id, postodtogas);
            if (sucesso)
            {
                return Ok(postodtogas);
            }
            return BadRequest("Erro ao editar os valores dos combustíveis");
        }
        [Authorize]
        [HttpDelete("deletar")]
        public IActionResult Remover(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            using var session = sessionFactory.OpenSession();
            var donoposto = session.Get<DonoPosto>(donopostoId);
            var posto = postoService.GetPorId(id);
            if (posto == null)
            {
                return NotFound();
            }
            if (donoposto.Postos.Any(p => p.PostoId == posto.PostoId))
            {
                postoService.Remover(id);
                return Ok(posto);
            }
            return BadRequest(donopostoId);
        }
    }
}
