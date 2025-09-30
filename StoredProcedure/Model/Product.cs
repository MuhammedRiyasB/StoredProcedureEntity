using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace StoredProcedure.Model
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Precision(18, 2)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        
    }
}
