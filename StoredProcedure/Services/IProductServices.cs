using StoredProcedure.Model;

namespace StoredProcedure.Services
{
    public interface IProductServices
    {
        Task<List<Product>> GetAll();
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(int id);

    }
}
