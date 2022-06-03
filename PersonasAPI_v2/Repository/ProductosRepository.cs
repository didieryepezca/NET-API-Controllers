using PersonasAPI_v2.Configuration;
using PersonasAPI_v2.Entities;
using Microsoft.EntityFrameworkCore;

namespace PersonasAPI_v2.Repository
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductosRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Productos>> GetAllProductos()
        {
            using (var db = _appDbContext)
            {
                IQueryable<Productos> query = db.Productos.Include(c => c.ProductoCategoria);

                return await query.ToListAsync();
                //return await db.Productos.OrderBy(f => f.NOMBRE).ToListAsync();
            }
        }

        public async Task<IEnumerable<ProductoCategoria>> GetAllCategorias()
        {
            using (var db = _appDbContext)
            {
                IQueryable<ProductoCategoria> query = db.ProductoCategoria;

                return await query.ToListAsync();
                //return await db.Productos.OrderBy(f => f.NOMBRE).ToListAsync();
            }
        }

        public async Task<int> InsertProductCategory(ProductoCategoria productoCategoria) 
        {
            var result = 0;
            using (var db = _appDbContext) {

                await db.AddAsync(productoCategoria);
                result = db.SaveChanges();                       
            }
            return result;               
        }

        public async Task<ProductoCategoria> GetCategoriaById(int idCategory)
        {
            var result = new ProductoCategoria();
            {
                using (var db = _appDbContext)
                {
                    result = await db.ProductoCategoria.Where(item => item.ID_PRODCATEGORIA == idCategory).FirstAsync();
                }
            }
            return result;
        }

        public async Task<int> InsertProductWithCategory(Productos productos)
        {
            var result = 0;
            using (var db = _appDbContext)
            {
                await db.AddAsync(productos);
                result = db.SaveChanges();
            }
            return result;
        }

        public async Task<Productos> GetProductById(int idProduct)
        {
            var result = new Productos();
            {
                using (var db = _appDbContext)
                {
                    result = await db.Productos.Include(c => c.ProductoCategoria).Where(item => item.ID_PRODUCTO == idProduct).FirstAsync();
                }
            }
            return result;
        }       

    }
}
