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
<<<<<<< HEAD
    [Route("api/[controller]")]
=======
    [Route("api")]
>>>>>>> b27b432 (Adicionando API)
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
<<<<<<< HEAD
        [HttpGet("getporpreco")]
        public IActionResult GetPostos()
        {
            try
            {
                var postos = postoService.GetPostos();
=======
        [HttpGet("gasoline")]
        public async Task<IActionResult> GetPostosPorGasolina()
        {
            try
            {
                var postos = await postoService.GetGasStationsByGasoline();
>>>>>>> b27b432 (Adicionando API)
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
<<<<<<< HEAD
        [HttpGet("getpordiesel")]
        public IActionResult GetPostos2()
        {
            try
            {
                var postos = postoService.GetPostos2();
=======
        [HttpGet("diesel")]
        public async Task<IActionResult> GetPostosPorDiesel()
        {
            try
            {
                var postos = await postoService.GetGasStationsByDiesel();
>>>>>>> b27b432 (Adicionando API)
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
<<<<<<< HEAD
        [HttpGet("getporetanol")]
        public IActionResult GetPostos3()
        {
            try
            {
                var postos = postoService.GetPostos3();
=======
        [HttpGet("ethanol")]
        public async Task<IActionResult> GetPostosPorEtanol()
        {
            try
            {
                var postos = await postoService.GetGasStationByEthanol();
>>>>>>> b27b432 (Adicionando API)
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
<<<<<<< HEAD
        [HttpGet("getporid")]
        public IActionResult GetPostos4()
        {
            try
            {
                var postos = postoService.GetPostos4();
=======
        [HttpGet("postos")]
        public async Task<IActionResult> GetNewPostos()
        {
            try
            {
                var postos = await postoService.GetNewGasStations();
>>>>>>> b27b432 (Adicionando API)
                return Ok(postos);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [Authorize]
<<<<<<< HEAD
        [HttpPut("editar")]
        public IActionResult Editar(int id, [FromBody] PostoDTO posto)
=======
        [HttpPut("posto/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PostoDTO posto)
>>>>>>> b27b432 (Adicionando API)
        {
            if (posto == null)
            {
                return BadRequest("Você não passou um posto");
            }
<<<<<<< HEAD
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
=======
            try
            {
                var Posto = await postoService.EditGasStation(id, posto);
                return Ok(posto);
            }
            catch(Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [Authorize]
        [HttpPut("prices/{id}")]
        public async Task<IActionResult> EditFuelPrices(int id, [FromBody] PostoDTOGas postodtogas)
>>>>>>> b27b432 (Adicionando API)
        {
            if (postodtogas == null)
            {
                return BadRequest("Você não passou um posto");
            }
<<<<<<< HEAD
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
=======
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
>>>>>>> b27b432 (Adicionando API)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Usuário não identificado.");
            }
            int donopostoId = int.Parse(userIdClaim.Value);
            using var session = sessionFactory.OpenSession();
<<<<<<< HEAD
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
=======
            var donoposto = await session.GetAsync<DonoPosto>(donopostoId);
            if (donoposto == null)
            {
                return NotFound("Dono do posto não encontrado");
            }
            var posto = await postoService.GetGasStationById(id);
            if (posto == null)
            {
                return NotFound("Posto com esse id não encontrado entre seus postos");
            }
            if (donoposto.Postos.Any(p => p.PostoId == posto.PostoId))
            {
                await postoService.RemoveGasStation(id);
                return Ok(posto);
            }
            return BadRequest();
>>>>>>> b27b432 (Adicionando API)
        }
    }
}
