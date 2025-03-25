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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(new Response
            {
                Status = 200,
                Message = "Productos obtenidos satisfactoriamente.",
                Content = await Service.ObtenerProductos()
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCreacionDto producto)
        {
            try
            {
                return Created("", new Response
                {
                    Status = 201,
                    Message = "Producto creado satisfactoriamente",
                    Content = await Service.CrearProducto(producto)
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
        public async Task<IActionResult> Modificar([FromRoute] int id, [FromBody] ProductoModificacionDto producto)
        {
            try
            {
                ProductoShowDto productoExistente = await Service.ModificarProducto(id, producto);

                if (productoExistente == null)
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
                    Content = productoExistente
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

        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute] int id)
        {
            try
            {
                ProductoShowDto producto = await Service.ObtenerProductoPorId(id);

                if (producto == null)
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
                    Content = producto
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
