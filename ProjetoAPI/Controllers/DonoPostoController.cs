using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Dtos;
using ProjetoAPI.Entidades;
using ProjetoAPI.Services;
using System.Security.Claims;

namespace ProjetoAPI.Controllers
{
    [Route("api")]
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
        [HttpGet("donoposto/postos")]
        public async Task<IActionResult> GetGasStations()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
                var postos = await donoPostoService.ShowGasStations(donopostoId);
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [Authorize]
        [HttpGet("donoposto")]
        public async Task<IActionResult> GetInfos()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
                var donoposto = await donoPostoService.ShowInfos(donopostoId);
                return Ok(donoposto);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpPost("donoposto")]
        public async Task<IActionResult> RegisterDonoPosto([FromBody] DonoPosto donoPosto)
        {
            if (await donoPostoService.ValidateCredentials(donoPosto.Email, donoPosto.SenhaHasheada))
            {
                return Conflict("Usuário já existe");
            }
            try
            {
                var donoposto = await donoPostoService.CreateDonoPosto(donoPosto);
                return Ok(donoposto);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DonoPostoDTO donoPosto)
        {
            var donoPostoExistente = await donoPostoService.SearchForEmail(donoPosto.email);
            bool senhaCorreta = donoPostoExistente != null
                ? BCrypt.Net.BCrypt.Verify(donoPosto.senha, donoPostoExistente.SenhaHasheada)
                : BCrypt.Net.BCrypt.Verify(donoPosto.senha, BCrypt.Net.BCrypt.HashPassword("senha_falsa"));
            if (!senhaCorreta)
            {
                return Unauthorized("Invalid credentials.");
            }
            var token = await tokenService.GenerateJwtToken(donoPosto.email);
            return Ok(new { Token = token });
        }
        [Authorize]
        [HttpPost("posto")]
        public async Task<IActionResult> Create([FromBody] Posto posto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
                var Posto = await donoPostoService.CreateGasStation(donopostoId, posto);
                return Ok(posto);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [Authorize]
        [HttpPut("donoposto")]
        public async Task<IActionResult> EditDono([FromBody] DonoPosto donoposto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
                var donoPosto = await donoPostoService.EditDonoPosto(donopostoId, donoposto);
                return Ok(donoPosto);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

        }
        [Authorize]
        [HttpDelete("donoposto")]
        public async Task<IActionResult> DeleteDono()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
                var donoposto = await donoPostoService.DeleteDonoPosto(donopostoId);
                return Ok(donoposto);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}