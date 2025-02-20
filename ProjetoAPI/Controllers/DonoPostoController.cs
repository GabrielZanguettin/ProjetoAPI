using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAPI.Dtos;
using ProjetoAPI.Entidades;
using ProjetoAPI.Services;
using System.Security.Claims;

namespace ProjetoAPI.Controllers
{
<<<<<<< HEAD
    [Route("api/[controller]")]
=======
    [Route("api")]
>>>>>>> b27b432 (Adicionando API)
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
<<<<<<< HEAD
        [HttpGet("postos")]
        public IActionResult ObterPostos()
=======
        [HttpGet("donoposto/postos")]
        public async Task<IActionResult> GetGasStations()
>>>>>>> b27b432 (Adicionando API)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
<<<<<<< HEAD
                var postos = donoPostoService.MostrarPostos(donopostoId);
                return Ok(postos);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
=======
                var postos = await donoPostoService.ShowGasStations(donopostoId);
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
>>>>>>> b27b432 (Adicionando API)
            }
        }
        [Authorize]
        [HttpGet("donoposto")]
<<<<<<< HEAD
        public IActionResult ObterInfo()
=======
        public async Task<IActionResult> GetInfos()
>>>>>>> b27b432 (Adicionando API)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
<<<<<<< HEAD
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
=======
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
            catch(Exception error)
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
                return Unauthorized("Credenciais inválidas");
            }
            var token = await tokenService.GenerateJwtToken(donoPosto.email);
            return Ok(new { Token = token });
        }
        [Authorize]
        [HttpPost("posto")]
        public async Task<IActionResult> Create([FromBody] Posto posto)
>>>>>>> b27b432 (Adicionando API)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            try
            {
<<<<<<< HEAD
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
=======
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
>>>>>>> b27b432 (Adicionando API)
        {   
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
<<<<<<< HEAD
            var donoposto = donoPostoService.Remover(donopostoId);
            if (donoposto == null)
            {
                return NotFound();
            }
            return Ok(donoposto);
=======
            try
            {
                var donoposto = await donoPostoService.DeleteDonoPosto(donopostoId);
                return Ok(donoposto);
            }
            catch(Exception error)
            {
                return BadRequest(error.Message);
            }
>>>>>>> b27b432 (Adicionando API)
        }
    }
}
