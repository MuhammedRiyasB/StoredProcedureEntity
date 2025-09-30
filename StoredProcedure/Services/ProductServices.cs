using StoredProcedure.Data;
using StoredProcedure.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StoredProcedure.Services
{
    public class ProductServices : IProductServices
    {
        public readonly AppDbContext _context;

        public ProductServices(AppDbContext context)
        {
            _context = context;

        }

        public Task<List<Product>> GetAll() => _context.ExecuteProductSP("READALL");

        public Task Create(Product product) => _context.ExecuteProductSP("CREATE", product);

        public Task Update(Product product) => _context.ExecuteProductSP("UPDATE", product);

        public Task Delete(int id) => _context.ExecuteProductSP("DELETE", new Product { Id = id });


    }
}
