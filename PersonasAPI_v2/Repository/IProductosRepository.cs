using PersonasAPI_v2.Entities;

namespace PersonasAPI_v2.Repository
{
    public interface IProductosRepository
    {
        Task<IEnumerable<Productos>> GetAllProductos();

        Task<IEnumerable<ProductoCategoria>> GetAllCategorias();

        Task<IEnumerable<Productos>> GetProducsByCategory(int idCategory);

        Task<int> InsertProductWithCategory(Productos productos);

        Task<Productos> GetProductById(int idProduct);

        Task<int> InsertProductCategory(ProductoCategoria productoCategoria);

        Task<ProductoCategoria> GetCategoriaById(int idCategory);

        Task<int> UpdateCategoria(ProductoCategoria categoria);

        Task<int> UpdateProduct(Productos productos);

        Task<int> DeleteProduct(int idProduct);
    }
}
