using Microsoft.AspNetCore.Mvc;
using PersonasAPI_v2.Entities;
using PersonasAPI_v2.Repository;

namespace PersonasAPI_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosRepository _productoRepo;        

        public ProductosController(IProductosRepository productoRepo) {

            _productoRepo = productoRepo;            
        }

        [HttpGet]
        [Route("getproducts")] //-> /api/productos/getproductos
        public async Task<ActionResult<IEnumerable<Productos>>> GetProducts()               
        {
            try
            {
                var productos = await _productoRepo.GetAllProductos();
                if (productos.Count() == 0) {
                    return NotFound();                    
                }                
                return Ok(productos);
            }
            catch (Exception e) 
            {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }            
        }

        [HttpGet]
        [Route("getcategorys")] //-> /api/productos/getcategorias
        public async Task<ActionResult<IEnumerable<ProductoCategoria>>> GetCategorys()
        {
            try
            {
                var categorys = await _productoRepo.GetAllCategorias();
                if (categorys.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(categorys);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }
        }

        [HttpPost]
        [Route("addproduct")]
        public async Task<IActionResult> AddProductWithCategory([FromBody] Productos product) 
        {
            try
            {
                if (product is null)
                {
                    return BadRequest("No ingreso un producto");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Producto inválido");
                }

                var newProduct = await _productoRepo.InsertProductWithCategory(product);
                if (newProduct > 0)
                {
                    //return Accepted("Se añadió la categoria");
                    return CreatedAtRoute("GetProductById", new { idProduct = product.ID_PRODUCTO }, product);
                }
                else
                {
                    return BadRequest("No se pudo guardar la información");
                }
            }
            catch (Exception e) {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }           
        }

        [HttpGet("{idProduct}", Name = "GetProductById")]
        [Route("getproductbyid")] //-> /api/productos/getcategorybyid
        public async Task<ActionResult<Productos>> GetProductById([FromQuery] int idProduct)
        {
            try
            {
                var product = await _productoRepo.GetProductById(idProduct);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }
        }

        [HttpPost]
        [Route("addproductcategory")]
        public async Task<IActionResult> AddProductCategory([FromBody] ProductoCategoria prodCategory)
        {
            try
            {
                if (prodCategory is null) {

                    return BadRequest("No ingreso una categoria");
                }

                if (!ModelState.IsValid) {

                    return BadRequest("Categoria inválida");
                }

                var newProdCat = await _productoRepo.InsertProductCategory(prodCategory);
                if (newProdCat > 0)
                {
                    //return Accepted("Se añadió la categoria");
                    return CreatedAtRoute("GetCategoryById", new { idCategory = prodCategory.ID_PRODCATEGORIA }, prodCategory);
                }
                else {
                    return BadRequest("No se pudo guardar la información");                     
                }                
            }
            catch (Exception e) {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }           
        }

        [HttpGet("{idCategory}", Name = "GetCategoryById")]
        [Route("getcategorybyid")] //-> /api/productos/getcategorybyid
        public async Task<ActionResult<ProductoCategoria>> GetCategoryById([FromQuery] int idCategory)
        {
            try
            {
                var category = await _productoRepo.GetCategoriaById(idCategory);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }
        }

        [HttpPut("{idCategory:int}")]
        [Route("updatecategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] ProductoCategoria category)
        {
            try
            {           
                if (category is null)
                {
                    return BadRequest("No ingreso una categoria");
                }

                var categToUp = _productoRepo.GetCategoriaById(category.ID_PRODCATEGORIA);
                if (categToUp == null)
                {
                    return NotFound($"Categoria con Id = {category.ID_PRODCATEGORIA} no hallada.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Categoria inválida");
                }

                var categoryUp = await _productoRepo.UpdateCategoria(category);
                if (categoryUp > 0)
                {
                    return AcceptedAtRoute("GetCategoryById", new { idCategory = category.ID_PRODCATEGORIA }, categoryUp);                    
                }
                else {
                    return BadRequest("No se pudo guardar la información");
                }
            }
            catch (Exception e) {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }        
        }

    }
}
