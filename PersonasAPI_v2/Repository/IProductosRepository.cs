using PersonasAPI_v2.Entities;

namespace PersonasAPI_v2.Repository
{
    public interface IProductosRepository
    {
        Task<IEnumerable<Productos>> GetAllProductos();

        Task<IEnumerable<ProductoCategoria>> GetAllCategorias();

        Task<int> InsertProductWithCategory(Productos productos);

        Task<Productos> GetProductById(int idProduct);

        Task<int> InsertProductCategory(ProductoCategoria productoCategoria);

        Task<ProductoCategoria> GetCategoriaById(int idCategory);

        Task<int> UpdateCategoria(ProductoCategoria categoria);

    }
}
