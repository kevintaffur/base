using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public partial class Producto
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string Descripcion { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string Estado { get; set; } = null!;
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        [InverseProperty("Productos")]
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
