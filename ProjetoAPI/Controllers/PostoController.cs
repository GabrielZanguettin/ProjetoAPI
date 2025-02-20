using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using ProjetoAPI.Dtos;
using ProjetoAPI.Entidades;
using ProjetoAPI.Services;
using System.Security.Claims;
using Org.BouncyCastle.Crypto.Generators;

namespace ProjetoAPI.Controllers
{
    [Route("api")]
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
        [HttpGet("gasoline")]
        public async Task<IActionResult> GetPostosPorGasolina()
        {
            try
            {
                var postos = await postoService.GetGasStationsByGasoline();
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("diesel")]
        public async Task<IActionResult> GetPostosPorDiesel()
        {
            try
            {
                var postos = await postoService.GetGasStationsByDiesel();
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("ethanol")]
        public async Task<IActionResult> GetPostosPorEtanol()
        {
            try
            {
                var postos = await postoService.GetGasStationByEthanol();
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpGet("postos")]
        public async Task<IActionResult> GetNewPostos()
        {
            try
            {
                var postos = await postoService.GetNewGasStations();
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [Authorize]
        [HttpPut("posto/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PostoDTO posto)
        {
            if (posto == null)
            {
                return BadRequest("Você não passou um posto");
            }
            try
            {
                var Posto = await postoService.EditGasStation(id, posto);
                return Ok(posto);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [Authorize]
        [HttpPut("prices/{id}")]
        public async Task<IActionResult> EditPrices(int id, [FromBody] PostoDTOGas postodtogas)
        {
            if (postodtogas == null)
            {
                return BadRequest("Você não passou um posto");
            }
            try
            {
                await postoService.EditPrices(id, postodtogas);
                return Ok(postodtogas);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [Authorize]
        [HttpDelete("posto/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            using var session = sessionFactory.OpenSession();
            var donoposto = await session.GetAsync<DonoPosto>(donopostoId);
            if (donoposto == null)
            {
                return Unauthorized("Dono do posto não encontrado.");
            }
            var posto = await postoService.GetGasStationById(id);
            if (posto == null)
            {
                return NotFound();
            }
            if (donoposto.Postos.Any(p => p.PostoId == posto.PostoId))
            {
                await postoService.RemoveGasStation(id);
                return Ok(posto);
            }
            return BadRequest();
        }
    }
}