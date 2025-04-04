using api.Dtos.Productos;
using api.Services;
using api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/productos")]
    public class ProductoController : ControllerBase
    {
        private IProductoService Service;

        public ProductoController(IProductoService service)
        {
            Service = service;
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(new Response
            {
                Status = 200,
                Message = "Productos obtenidos satisfactoriamente.",
                Content = await Service.ObtenerTodos()
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCreacionDto entidadCreacionDto)
        {
            try
            {
                return Created("", new Response
                {
                    Status = 201,
                    Message = "Producto creado satisfactoriamente",
                    Content = await Service.Crear(entidadCreacionDto)
                });
            }
            catch
            {
                return BadRequest(new Response
                {
                    Status = 400,
                    Message = "No se pudo crear el producto con los datos proporcionados.",
                    Content = null
                });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Modificar([FromRoute] int id, [FromBody] ProductoModificacionDto entidadModificacionDto)
        {
            try
            {
                ProductoShowDto entidadExistente = await Service.Modificar(id, entidadModificacionDto);

                if (entidadExistente == null)
                {
                    return NotFound(new Response
                    {
                        Status = 404,
                        Message = "Producto no existe.",
                        Content = null
                    });
                }
                return Ok(new Response
                {
                    Status = 200,
                    Message = "Producto modificado de forma satisfactoria.",
                    Content = entidadExistente
                });
            }
            catch
            {
                return BadRequest(new Response
                {
                    Status = 400,
                    Message = "No se pudo modificar el producto con los datos proporcionados.",
                    Content = null
                });
            }
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute] int id)
        {
            try
            {
                ProductoShowDto entidad = await Service.ObtenerPorId(id);

                if (entidad == null)
                {
                    return NotFound(new Response
                    {
                        Status = 404,
                        Message = "Producto no existe",
                        Content = null
                    });
                }
                return Ok(new Response
                {
                    Status = 200,
                    Message = "Producto obtenido.",
                    Content = entidad
                });
            }
            catch
            {
                return NotFound(new Response
                {
                    Status = 404,
                    Message = "Producto no existe.",
                    Content = null
                });
            }
        }
    }
}
