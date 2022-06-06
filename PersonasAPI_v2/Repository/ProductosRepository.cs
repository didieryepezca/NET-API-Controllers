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
            IQueryable<Productos> query = _appDbContext.Productos;

            return await query.ToListAsync();
            //return await db.Productos.OrderBy(f => f.NOMBRE).ToListAsync();
        }

        public async Task<IEnumerable<ProductoCategoria>> GetAllCategorias()
        {
            IQueryable<ProductoCategoria> query = _appDbContext.ProductoCategoria;

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Productos>> GetProducsByCategory(int idCategory)
        {
            IQueryable<Productos> query = _appDbContext.Productos;

            query.Where(item => item.ID_PRODCATEGORIA == idCategory).Include(c => c.ProductoCategoria);

            return await query.ToListAsync();
        }

        public async Task<int> InsertProductCategory(ProductoCategoria productoCategoria)
        {
            var result = 0;

            await _appDbContext.AddAsync(productoCategoria);
            result = _appDbContext.SaveChanges();

            return result;
        }

        public async Task<ProductoCategoria> GetCategoriaById(int idCategory)
        {
            var result = new ProductoCategoria();
            {
                result = await _appDbContext.ProductoCategoria.Where(item => item.ID_PRODCATEGORIA == idCategory).FirstAsync();
            }
            return result;
        }

        public async Task<int> InsertProductWithCategory(Productos productos)
        {
            var result = 0;
            {
                await _appDbContext.AddAsync(productos);
                result = _appDbContext.SaveChanges();
            }
            return result;
        }

        public async Task<Productos> GetProductById(int idProduct)
        {
            var result = new Productos();
            {               
               result = await _appDbContext.Productos.Where(item => item.ID_PRODUCTO == idProduct).FirstAsync();               
            }
            return result;
        }

        public async Task<int> UpdateCategoria(ProductoCategoria categoria)
        {
            var result = 0;           
            var cate = await _appDbContext.ProductoCategoria.Where(item => item.ID_PRODCATEGORIA == categoria.ID_PRODCATEGORIA).FirstAsync();

            cate.NOMBRE = categoria.NOMBRE;
            cate.FECHA_CREACION = categoria.FECHA_CREACION;

            result = await _appDbContext.SaveChangesAsync();
           
            return result;
        }

        public async Task<int> UpdateProduct(Productos productos)
        {
            var result = 0;
            var prod = await _appDbContext.Productos.Where(item => item.ID_PRODUCTO == productos.ID_PRODUCTO).FirstAsync();

            prod.ID_PRODCATEGORIA = productos.ID_PRODCATEGORIA;
            prod.NOMBRE = productos.NOMBRE;
            prod.DESCRIPCION = productos.DESCRIPCION;
            prod.STOCK = productos.STOCK;
            prod.UNIDAD_MEDIDA = productos.UNIDAD_MEDIDA;
            prod.STOCK_MIN = productos.STOCK_MIN;
            prod.USUARIO = productos.USUARIO;

            result = await _appDbContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> DeleteProduct(int idProduct) {

            var result = 0;

            var prod = await _appDbContext.Productos.Where(item => item.ID_PRODUCTO == idProduct).FirstAsync();
            _appDbContext.Remove(prod);
            result = await _appDbContext.SaveChangesAsync();

            return result;
        }
    }
}