﻿using api.Dtos.Usuarios;

namespace api.Dtos.Productos
{
    public class ProductoShowDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; } = null!;
        public virtual UsuarioShowDto Usuario { get; set; }
    }
}
