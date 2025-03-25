﻿namespace front.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; } = null!;
        public virtual Usuario Usuario { get; set; }
    }
}
