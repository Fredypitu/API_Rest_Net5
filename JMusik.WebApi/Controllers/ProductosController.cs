using AutoMapper;
using JMusik.Data;
using JMusik.Data.Contratos;
using JMusik.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController: ControllerBase
    {
        private readonly IProductoRepositorio _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductosController> _logger;
        public ProductosController(IProductoRepositorio repo, IMapper mapper, ILogger<ProductosController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Productos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
        {
            try
            {
                var producto = await _repo.ObtenerAll();
                return _mapper.Map<List<ProductoDto>>(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)} {ex.Message}");
                return BadRequest();    
            }
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {
            try
            {
                var producto = await _repo.ObtenerById(id);

                if (producto == null)
                {
                    _logger.LogWarning($"En controlador {nameof(Get)} no se encontró Producto {id}");
                    return NotFound();
                }

                return _mapper.Map<ProductoDto>(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en controlador {nameof(Get)}: {ex.Message}");
                return BadRequest();
            }
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> Post(ProductoDto productoDto)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDto);

                if (await _repo.Agregar(producto) is null)
                {

                    return BadRequest();
                }

                var productoDtoNuevo = _mapper.Map<ProductoDto>(producto);
                return CreatedAtAction(nameof(Get), new { id = productoDtoNuevo.Id }, productoDtoNuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Post)}: {ex.Message}");
                return BadRequest();
            }

        }


        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<ProductoDto>> Put(ProductoDto productoDto)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDto);
                if (productoDto is null || !await _repo.Modificar(producto))
                {
                    _logger.LogWarning($"No existe el producto con id: {producto.Id}");
                    return NotFound();
                }                            

                var nuevoProductoDto = _mapper.Map<ProductoDto>(producto);

                return CreatedAtAction("Get", new { nuevoProductoDto.Id}, nuevoProductoDto);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Put)} producto {ex.Message}");
                return BadRequest();
            }


        }


        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var prod = await _repo.ObtenerById(id);
                if (await _repo.Eliminar(prod))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }


        }

    }
}
