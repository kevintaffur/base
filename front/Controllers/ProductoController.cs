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

            List<Producto> productos = await ProductoService.ObtenerTodos(token);
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
            Producto producto = await ProductoService.ObtenerPorId(id, token);

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
        public async Task<ActionResult> Create(ProductoCreacionDto entidadCreacionDto)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");

                if (!await ProductoService.Crear(entidadCreacionDto, token))
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

            Producto entidad = await ProductoService.ObtenerPorId(id, token);
            if (entidad == null)
            {
                return RedirectToAction("Index", "Producto");
            }

            ProductoModificacionDto entidadMod = new ProductoModificacionDto
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                Descripcion = entidad.Descripcion,
                Estado = entidad.Estado,
                UsuarioId = entidad.Usuario.Id
            };

            return View(entidadMod);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductoModificacionDto entidadModificacionDto)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");
                if (!await ProductoService.Modificar(entidadModificacionDto.Id, entidadModificacionDto, token))
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

            Producto entidad = await ProductoService.ObtenerPorId(id, token);

            if (entidad == null)
            {
                return RedirectToAction("Index", "Producto");
            }

            return View(entidad);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");
                Producto entidad = await ProductoService.ObtenerPorId(id, token);

                if (entidad == null)
                {
                    return RedirectToAction("Index", "Producto");
                }

                ProductoModificacionDto e = new ProductoModificacionDto
                {
                    Descripcion = entidad.Descripcion,
                    Estado = "N",
                    Id = entidad.Id,
                    Nombre = entidad.Nombre,
                    UsuarioId = entidad.Usuario.Id
                };

                if (!await ProductoService.Modificar(id, e, token))
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
