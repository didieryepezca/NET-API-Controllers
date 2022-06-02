using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonasAPI_v2.Entities
{
    public class ProductoCategoria
    {
        [Key]
        public int ID_PRODCATEGORIA { get; set; }

        [Display(Name = "CATEGORIA")]
        public string NOMBRE { get; set; }

        [Display(Name = "FECHA DE CREACION")]
        public DateTime FECHA_CREACION { get; set; }

        [Display(Name = "USUARIO")]
        public string USUARIO { get; set; }
        public ICollection<Productos> Productos { get; set; }
    }
}
