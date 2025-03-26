using System.Threading.Tasks;
using front.Dtos.Productos;
using front.Models;
using front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace front.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService ProductoService;
        private readonly AuthService AuthService;

        public ProductoController(IProductoService productoService, AuthService authService)
        {
            ProductoService = productoService;
            AuthService = authService;
        }

        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            //string rol = AuthService.ExtraerRol(token);

            List<Producto> productos = await ProductoService.ObtenerProductos(token);
            return View(productos);
        }

        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult> Details(int id)
        {
            string token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            Producto producto = await ProductoService.ObtenerProductoPorId(id, token);

            if (producto == null)
            {
                return RedirectToAction("Index", "Producto");
            }

            return View(producto);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            string token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductoCreacionDto productoCreacionDto)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");

                if (!await ProductoService.CrearProducto(productoCreacionDto, token))
                {
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            string token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            Producto producto = await ProductoService.ObtenerProductoPorId(id, token);
            if (producto == null)
            {
                return RedirectToAction("Index", "Producto");
            }

            ProductoModificacionDto productoMod = new ProductoModificacionDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Estado = producto.Estado,
                UsuarioId = producto.Usuario.Id
            };

            return View(productoMod);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductoModificacionDto productoModificacionDto)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");
                if (!await ProductoService.ModificarProducto(productoModificacionDto.Id, productoModificacionDto, token))
                {
                    return View();
                }
                return RedirectToAction("Index", "Producto");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            Producto producto = await ProductoService.ObtenerProductoPorId(id, token);

            if (producto == null)
            {
                return RedirectToAction("Index", "Producto");
            }

            return View(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");
                Producto producto = await ProductoService.ObtenerProductoPorId(id, token);

                if (producto == null)
                {
                    return RedirectToAction("Index", "Producto");
                }

                ProductoModificacionDto pr = new ProductoModificacionDto
                {
                    Descripcion = producto.Descripcion,
                    Estado = "N",
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    UsuarioId = producto.Usuario.Id
                };

                if (!await ProductoService.ModificarProducto(id, pr, token))
                {
                    return View();
                }

                return RedirectToAction("Index", "Producto");
            }
            catch
            {
                return View();
            }
        }
    }
}
