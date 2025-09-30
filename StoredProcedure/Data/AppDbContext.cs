using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using StoredProcedure.Model;

namespace StoredProcedure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }


        public DbSet<Product> Products { get; set; }
       
        public async Task<List<Product>> ExecuteProductSP(string action, Product product = null)
        {
            var actionParam = new SqlParameter("@ActionType", action);
            var idParam = new SqlParameter("@Id", product?.Id ?? (object)DBNull.Value);
            var nameParam = new SqlParameter("@Name", product?.Name ?? (object)DBNull.Value);
            var PriceParam = new SqlParameter("@Price", product?.Price ?? (object)DBNull.Value);
            var stockParam = new SqlParameter("@Stock", product?.Stock ?? (object)DBNull.Value);
            var rowsAffectedParam = new SqlParameter("@RowsAffected", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            if(action == "READALL")
            {
                var products = await Products
                    .FromSqlRaw("Execute dbo.ManageProducts @ActionType,@Id, @Name, @Price, @Stock, @RowsAffected OUTPUT",
                    actionParam, idParam, nameParam, PriceParam, stockParam, rowsAffectedParam)
                    .ToListAsync();

                return products;
            }

            else
            {
                await Database.ExecuteSqlRawAsync(
                    "Execute dbo.ManageProducts @ActionType,@Id, @Name, @Price, @Stock, @RowsAffected OUTPUT",
                    actionParam, idParam, nameParam, PriceParam, stockParam, rowsAffectedParam);

                var affectedRows = (int)(rowsAffectedParam.Value ?? 0);
                
                if(affectedRows ==0) 
                    throw new InvalidOperationException($"{action} Operation failed or no rows affected.");

                return new List<Product>();
            }
           
        }

    }
}
