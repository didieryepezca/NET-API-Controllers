using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonasAPI_v2.Entities;
using PersonasAPI_v2.Repository;
using PersonasAPI_v2.Services;

namespace PersonasAPI_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosRepository _productoRepo;
        private readonly IJwtService _jwtService;

        public ProductosController(IProductosRepository productoRepo, IJwtService jwtService)
        {
            _productoRepo = productoRepo;
            _jwtService = jwtService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("generarsession")] //-> /api/productos/getproductos
        public string AuthApiWithJWT()
        {
            try
            {
                var token =  _jwtService.GenerateToken();
                if (token != string.Empty)
                {
                    return token;
                }
                else {
                    return "No se ha podido generar un token";
                }               
            }
            catch (Exception e)
            {
                return "Error interno del servidor.. " + e.Message;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getproducts")] //-> /api/productos/getproductos
        public async Task<ActionResult<IEnumerable<Productos>>> GetProducts()
        {
            try
            {
                var productos = await _productoRepo.GetAllProductos();
                if (productos.Count() == 0)
                {
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
        [Authorize]
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
        [Authorize]
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
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }
        }

        [HttpGet("{idProduct}", Name = "GetProductById")]
        [Authorize]
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
        [Authorize]
        [Route("addproductcategory")]
        public async Task<IActionResult> AddProductCategory([FromBody] ProductoCategoria prodCategory)
        {
            try
            {
                if (prodCategory is null)
                {

                    return BadRequest("No ingreso una categoria");
                }

                if (!ModelState.IsValid)
                {

                    return BadRequest("Categoria inválida");
                }

                var newProdCat = await _productoRepo.InsertProductCategory(prodCategory);
                if (newProdCat > 0)
                {
                    //return Accepted("Se añadió la categoria");
                    return CreatedAtRoute("GetCategoryById", new { idCategory = prodCategory.ID_PRODCATEGORIA }, prodCategory);
                }
                else
                {
                    return BadRequest("No se pudo guardar la información");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }
        }

        [HttpGet("{idCategory}", Name = "GetCategoryById")]
        [Authorize]
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

        [HttpPut]
        [Authorize]
        [Route("updatecategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] ProductoCategoria category)
        {
            try
            {
                if (category is null)
                {
                    return BadRequest("No ingreso una categoria");
                }

                var catefind = await _productoRepo.GetCategoriaById(category.ID_PRODCATEGORIA);
                if (catefind == null)
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
                    return AcceptedAtRoute("GetCategoryById", new { idCategory = category.ID_PRODCATEGORIA }, category);
                }
                else
                {
                    return BadRequest("No se pudo actualizar la información");
                }
            }
            catch (Exception e)
            {

                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("updateproduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Productos producto)
        {
            try
            {
                if (producto is null)
                {
                    return BadRequest("No ingreso una categoria");
                }

                var prodFind = await _productoRepo.GetProductById(producto.ID_PRODUCTO);
                if (prodFind == null)
                {
                    return NotFound($"Producto con Id = {producto.ID_PRODUCTO} no hallado.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Producto inválido");
                }

                var productUp = await _productoRepo.UpdateProduct(producto);
                if (productUp > 0)
                {
                    return AcceptedAtRoute("GetProductById", new { idProduct = producto.ID_PRODUCTO }, producto);
                }
                else
                {
                    return BadRequest("No se pudo actualizar la información");
                }
            }
            catch (Exception e)
            {

                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteproduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int idProduct)
        {
            try
            {
                var prodFind = await _productoRepo.GetProductById(idProduct);
                if (prodFind == null)
                {
                    return NotFound($"Producto con Id = {idProduct} no hallado.");
                }

                var productDe = await _productoRepo.DeleteProduct(idProduct);
                if (productDe > 0)
                {
                    return Accepted();
                }

                return Accepted();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getproductsbycategoryid")]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductsByCategoryId([FromQuery] int cateId) 
        {
            try
            {
                var prodsByCat = await _productoRepo.GetProducsByCategory(cateId);
                if (prodsByCat == null)
                {
                    return NotFound();
                }

                return Ok(prodsByCat);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno del servidor.. " + e.Message);
            }
        }


    }
}
