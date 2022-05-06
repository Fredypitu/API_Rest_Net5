using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JMusik.Data;
using JMusik.Data.Contratos;
using Microsoft.Extensions.Logging;
using JMusik.Dtos;
using AutoMapper;

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenRepositorio _repo;
        private readonly ILogger<OrdenesController> _logger;
        private readonly IMapper _mapper;
        public OrdenesController(IOrdenRepositorio repo, ILogger<OrdenesController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Ordens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenDto>>> Get()
        {
            try
            {
                var listaOrden = await _repo.ObtenerAll();
                return _mapper.Map<List<OrdenDto>>(listaOrden);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)}: {ex.Message}");
                return BadRequest();
            }
        }

        // GET: api/Ordens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenDto>> Get(int id)
        {
            try
            {
                var orden = await _repo.ObtenerById(id);

                if (orden == null)
                {
                    _logger.LogWarning($"No existe la orden con id: {id}");
                    return NotFound();
                }
                ; return _mapper.Map<OrdenDto>(orden);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)}: {ex.Message}");
                return BadRequest();
            }

        }

        // POST: api/Ordens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenDto>> Post(OrdenDto ordenDto)
        {
            try
            {
                var orden = _mapper.Map<JMusik.Models.Orden>(ordenDto);
                if (await _repo.Agregar(orden) is null)
                {
                    _logger.LogWarning($"No existe la orden con id: {ordenDto.Id}");
                    return NotFound();
                }

                var ordenDtoNuevo = _mapper.Map<OrdenDto>(orden);
                return CreatedAtAction("Get", new { id = ordenDtoNuevo.Id }, ordenDtoNuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: {ex.Message}");
                return BadRequest();
            }


        }


        // DELETE: api/Ordens/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var orden = await _repo.ObtenerById(id);
                if (orden is null)
                {
                    _logger.LogWarning($"No existe la orend con id: {id}");
                    return NotFound();
                }

                await _repo.Eliminar(orden);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Delete)}: {ex.Message}");
                return BadRequest();
            }
        }

    }
}
