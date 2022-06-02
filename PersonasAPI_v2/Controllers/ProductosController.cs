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
        [Route("getproductos")]
        public async Task<IEnumerable<Productos>> GetProductos()               
        {        

            var productos = await _productoRepo.GetAllProductos();       

            return productos;
        }

    }
}
