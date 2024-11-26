using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Dtos;
using ProjetoAPI.Entidades;
using ProjetoAPI.Services;
using System.Security.Claims;

namespace ProjetoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonoPostoController : ControllerBase
    {
        private readonly DonoPostoService donoPostoService;
        private readonly TokenService tokenService;
        public DonoPostoController(DonoPostoService donoPostoService, TokenService tokenService)
        {
            this.donoPostoService = donoPostoService;
            this.tokenService = tokenService;
        }
        [Authorize]
        [HttpGet("postos")]
        public IActionResult ObterPostos()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
                var postos = donoPostoService.MostrarPostos(donopostoId);
                return Ok(postos);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        [Authorize]
        [HttpGet("donoposto")]
        public IActionResult ObterInfo()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
                var donoposto = donoPostoService.MostrarInformacoes(donopostoId);
                return Ok(donoposto);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        [HttpPost("criarconta")]
        public IActionResult Registrar([FromBody] DonoPosto donoPosto)
        {
            if (donoPostoService.ValidarCredenciais(donoPosto.Email, donoPosto.SenhaHasheada))
            {
                return Conflict("Usuário já existe");
            }
            donoPostoService.CadastrarDonoPosto(donoPosto);
            return Ok("Sua conta foi criada!");
        }
        [HttpPost("logar")]
        public IActionResult Login([FromBody] DonoPostoDTO donoPosto)
        {
            if (donoPostoService.ValidarCredenciais(donoPosto.email, donoPosto.senha))
            {
                var token = tokenService.GenerateJwtToken(donoPosto.email);
                return Ok( new { Token = token});
            }
            return BadRequest();
        }
        [Authorize]
        [HttpPost("criarposto")]
        public IActionResult Criar([FromBody] Posto posto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            bool sucesso = donoPostoService.CriarPosto(donopostoId, posto);
            if (sucesso)
            {
                return Ok("Posto criado com sucesso!");
            }
            return BadRequest($"Falha ao criar posto");
        }
        [Authorize]
        [HttpPut("editardonoposto")]
        public IActionResult EditarDono([FromBody] DonoPosto donoposto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
                var donoPosto = donoPostoService.EditarDonoPosto(donopostoId, donoposto);
                return Ok(donoPosto);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }

        }
        [Authorize]
        [HttpDelete("deletar")]
        public IActionResult Deletar()
        {   
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            var donoposto = donoPostoService.Remover(donopostoId);
            if (donoposto == null)
            {
                return NotFound();
            }
            return Ok(donoposto);
        }
    }
}
