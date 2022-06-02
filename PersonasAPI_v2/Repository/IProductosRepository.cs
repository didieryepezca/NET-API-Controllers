using PersonasAPI_v2.Entities;

namespace PersonasAPI_v2.Repository
{
    public interface IProductosRepository
    {
        Task<IEnumerable<Productos>> GetAllProductos();
    }
}
