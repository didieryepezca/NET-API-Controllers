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
                //IQueryable<Personas> query = await db.Personas.OrderBy(f => f.NOMBRE);
                //return query.ToList();

                return await db.Productos.OrderBy(f => f.NOMBRE).ToListAsync();
            }
        }


    }
}
