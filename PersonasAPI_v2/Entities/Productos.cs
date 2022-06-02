using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonasAPI_v2.Entities
{
    public class Productos
    {
        [Key]
        public int ID_PRODUCTO { get; set; }

        [Display(Name = "ID_PRODCATEGORIA")]
        [ForeignKey("ProductoCategoria")]
        public int ID_PRODCATEGORIA { get; set; }
        public virtual ProductoCategoria ProductoCategoria { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [Display(Name = "NOMBRE")]
        public string NOMBRE { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripcion del producto")]
        [Display(Name = "DESCRIPCION")]
        public string DESCRIPCION { get; set; }

        [Required(ErrorMessage = "Ingrese Stock de Producto")]
        [Display(Name = "STOCK")]
        public decimal STOCK { get; set; }

        [Required(ErrorMessage = "Seleccione una Unidad de Medida")]
        [Display(Name = "UND MEDIDA")]
        public string UNIDAD_MEDIDA { get; set; }

        [Display(Name = "USUARIO")]
        public string USUARIO { get; set; }

        [Required(ErrorMessage = "Ingrese Stock Minimo de Producto")]
        [Display(Name = "STOCK MINIMO")]
        public decimal STOCK_MIN { get; set; }
    }
}
